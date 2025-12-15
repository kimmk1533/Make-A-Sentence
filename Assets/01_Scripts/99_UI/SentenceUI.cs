using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Sentence))]
public class SentenceUI : SerializedMonoBehaviour
{
	#region 기본 템플릿
	#region 변수
	private Sentence m_Sentence = null;
	private List<SentenceUIElement> m_SentenceUIElementList = null;

	[SerializeField]
	private Canvas m_Canvas = null;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static SentenceManager M_Sentence => SentenceManager.Instance;

	private static SentenceUIElementManager M_SentenceUIElement => SentenceUIElementManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public void Initialize()
	{
		m_Sentence = GetComponent<Sentence>();
		m_SentenceUIElementList = new List<SentenceUIElement>();

		List<string> textList = M_Sentence.GetSentenceText(m_Sentence);
		foreach (var text in textList)
		{
			SentenceUIElement sentenceUIElement = null;
			string template = text.Remove(text.Length - 1, 1).Remove(0, 1);

			if (text.StartsWith("{"))
			{
				sentenceUIElement = M_SentenceUIElement.GetBuilder("Blank")
					.SetParent(transform)
					.SetActive(true)
					.Spawn();

				Blank blank = sentenceUIElement as Blank;

				if (template.Equals("Subject"))
					blank.deployableType = E_WordType.Player | E_WordType.Enemy | E_WordType.Magic;
				if (template.Equals("Target"))
					blank.deployableType = E_WordType.Player | E_WordType.Enemy | E_WordType.Magic;
				if (template.Equals("Magic"))
					blank.deployableType = E_WordType.Magic;

				blank.template = template;
			}
			else
			{
				sentenceUIElement = M_SentenceUIElement.GetBuilder("Text")
					.SetParent(transform)
					.SetActive(true)
					.Spawn();

				SentenceUIText sentenceUIText = sentenceUIElement as SentenceUIText;

				if (text.StartsWith("["))
				{
					sentenceUIText.text = m_Sentence.GetWordText(template);
				}
				else
				{
					sentenceUIText.text = text;
				}
			}

			m_SentenceUIElementList.Add(sentenceUIElement);
		}
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public void Finallize()
	{
		foreach (var item in m_SentenceUIElementList)
		{
			M_SentenceUIElement.Despawn(item);
		}
		m_SentenceUIElementList.Clear();
		m_SentenceUIElementList = null;
		m_Sentence = null;
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	public SentenceUIElement GetUIElementFrontBlank(Blank blank)
	{
		for (int i = 1; i < m_SentenceUIElementList.Count; ++i)
			if (m_SentenceUIElementList[i] == blank)
				return m_SentenceUIElementList[i - 1];

		Debug.Log("?");
		return null;
	}
}