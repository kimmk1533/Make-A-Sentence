using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordManager : ObjectManager<WordManager, Word>
{
	#region 기본 템플릿
	#region 변수
	// Game

	// UI
	private Word m_DummyWord = null;
	private LayoutElement m_DummyLayoutElement = null;
	#endregion

	#region 프로퍼티
	[field: SerializeField]
	public RectTransform inventoryPanel { get; set; }
	[field: SerializeField]
	public ScrollRect wordScrollView { get; set; }
	public RectTransform content => wordScrollView.content;
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 기본 초기화 함수 (Init Scene 진입 시, 즉 게임 실행 시 호출)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		m_DummyWord = GetBuilder("Word")
			.SetParent(transform)
			.SetActive(false)
			.Spawn();
		m_DummyWord.name = "Dummy Word";
		m_DummyWord.GetComponent<Image>().enabled = false;
		m_DummyLayoutElement = m_DummyWord.gameObject.AddComponent<LayoutElement>();
	}
	/// <summary>
	/// 기본 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{
		m_DummyWord.GetComponent<Image>().enabled = true;
		Despawn(m_DummyWord);

		base.Finallize();
	}

	/// <summary>
	/// 메인 초기화 함수 (본인 Main Scene 진입 시 호출)
	/// </summary>
	public override void InitializeMain()
	{
		base.InitializeMain();

		Word word = null;

		for (int i = 0; i < 6; ++i)
		{
			word = GetBuilder("Word")
			.SetParent(content)
			.SetActive(true)
			.Spawn();
			word.wordType = E_WordType.Player;
			word.ui.text = "플레이어";
		}

		for (int i = 0; i < 6; ++i)
		{
			word = GetBuilder("Word")
				.SetParent(content)
				.SetActive(true)
				.Spawn();
			word.wordType = E_WordType.Enemy;
			word.ui.text = "적";
		}

		for (int i = 0; i < 6; ++i)
		{
			word = GetBuilder("Word")
				.SetParent(content)
				.SetActive(true)
				.Spawn();
			word.wordType = E_WordType.Magic;
			word.magicTitle = "Fire Ball";
			word.ui.text = "파이어 볼";
		}
	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{


		base.FinallizeMain();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	public void SpawnDummyWord(WordUI wordUI)
	{
		m_DummyLayoutElement.minWidth = (wordUI.transform as RectTransform).sizeDelta.x;
		m_DummyWord.transform.SetParent(wordUI.transform.parent);
		m_DummyWord.transform.SetSiblingIndex(wordUI.transform.GetSiblingIndex());
		m_DummyWord.gameObject.SetActive(true);
	}
	public void DespawnDummyWord()
	{
		m_DummyWord.gameObject.SetActive(false);
		m_DummyWord.transform.SetParent(transform);
		m_DummyLayoutElement.minWidth = 0f;
	}
}