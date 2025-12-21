using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Word))]
public class WordUI : SerializedMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public enum E_State
	{
		WordInventory,
		Sentence,
	}

	#region 기본 템플릿
	#region 변수
	private Word m_Word = null;

	[SerializeField, ChildComponent("Text (TMP)")]
	private TextMeshProUGUI m_Text = null;

	private E_State m_State = E_State.WordInventory;

	private int m_PrevSiblingIndex = -1;
	private Blank m_Blank = null;
	#endregion

	#region 프로퍼티
	public string text { get => m_Text.text; set => m_Text.text = value; }
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static WordManager M_Word => WordManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public void Initialize()
	{
		m_Word = GetComponent<Word>();
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public void Finallize()
	{
		m_Blank = null;
		m_PrevSiblingIndex = -1;

		m_State = E_State.WordInventory;

		m_Word = null;
	}
	#endregion

	#region 유니티 콜백 함수
	public void OnBeginDrag(PointerEventData eventData)
	{
		// 좌클릭만 드래그 가능
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		// 인덱스(위치) 저장
		m_PrevSiblingIndex = transform.GetSiblingIndex();
		// 부모 저장
		Transform parent = transform.parent;

		// 부모 변경(Scroll View의 Viewport에서 벗어나기 위함)
		transform.SetParent(M_Word.wordScrollView.transform);

		switch (m_State)
		{
			// 단어 인벤토리에 있었으면
			case E_State.WordInventory:
				// 더미 단어 생성
				M_Word.SpawnDummyWord(this, parent, m_PrevSiblingIndex);
				break;
			// 문장에 들어가 있었으면
			case E_State.Sentence:
				// 문장에 변경사항 적용 (단어 제거)
				UpdateSentence(null);

				// 빈 칸 켜기
				m_Blank.gameObject.SetActive(true);
				// 레이아웃 업데이트
				LayoutRebuilder.ForceRebuildLayoutImmediate(m_Blank.sentecne.transform as RectTransform);
				break;
		}
	}
	public void OnDrag(PointerEventData eventData)
	{
		// 좌클릭만 드래그 가능
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		transform.position += (Vector3)eventData.delta;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		// 좌클릭만 드래그 가능
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		// 단어 인벤토리에 있었으면
		if (m_State == E_State.WordInventory)
		{
			// 더미 오브젝트 삭제
			M_Word.DespawnDummyWord();
		}

		// 빈 칸 찾기
		#region Find Blank
		Blank blank = null;
		List<RaycastResult> raycastResultList = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, raycastResultList);

		foreach (var item in raycastResultList)
		{
			blank = item.gameObject.GetComponent<Blank>();
			if (blank != null)
				break;
		}

		m_Blank = blank;
		#endregion

		// 빈 칸에 배치할 수 없으면
		if (CheckDeployOnBlank() == false)
		{
			// 부모 변경(단어 인벤토리로 돌아감)
			transform.SetParent(M_Word.content);
			// 원래 단어 인벤토리에 있었으면
			if (m_State == E_State.WordInventory)
				// 위치도 기존 위치로 맞춰줌
				transform.SetSiblingIndex(m_PrevSiblingIndex);

			// 상태 업데이트
			m_State = E_State.WordInventory;
		}
		// 빈 칸에 배치할 수 있으면
		else
		{
			// 문장에 변경사항 적용 (단어 적용)
			UpdateSentence(m_Word);
			
			// 부모 변경(문장으로)
			transform.SetParent(m_Blank.transform.parent);
			// 빈 칸 위치로 이동
			transform.SetSiblingIndex(m_Blank.transform.GetSiblingIndex());
			// 빈 칸 끄기
			m_Blank.gameObject.SetActive(false);
			// 레이아웃 업데이트
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_Blank.sentecne.transform as RectTransform);

			// 상태 업데이트
			m_State = E_State.Sentence;
		}
	}
	#endregion
	#endregion

	private bool CheckDeployOnBlank()
	{
		if (m_Blank == null)
			return false;

		return m_Blank.CheckDeployable(m_Word.wordType);
	}
	private void UpdateSentence(Word word)
	{
		if (m_Blank == null)
			throw new System.NullReferenceException("m_Blank가 null임");

		switch (m_Blank.template)
		{
			case "Subject":
				m_Blank.sentecne.subjectWord = word;
				break;
			case "Target":
				m_Blank.sentecne.targetWord = word;
				break;
			case "Magic":
				m_Blank.sentecne.magicWord = word;
				break;
		}

		// 단어가 "플레이어" 였을 때 앞의 UI Element(SelectingType) 오브젝트 키거나 끄기
		SentenceUIElement uiElement = m_Blank.sentecne.ui.GetUIElementFrontBlank(m_Blank);
		if (word != null &&
			word.wordType == E_WordType.Player)
			uiElement?.gameObject.SetActive(false);
		else
			uiElement?.gameObject.SetActive(true);
	}
}