using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum E_SelectingType
{
	// 가장 가까운 (1)
	Nearest,
	// 랜덤 (1 ~ all)
	Random,
	// 주변 (all)
	Around,

	Max
}

[RequireComponent(typeof(SentenceUI))]
public abstract class Sentence : ObjectPoolItem<Sentence>
{
	#region 기본 템플릿
	#region 변수

	// UI
	protected SentenceUI m_SentenceUI = null;

	protected Dictionary<string, System.Func<string>> m_WordTextMap = null;
	#endregion

	#region 프로퍼티
	[field: SerializeField]
	public Word subjectWord { get; set; }
	[field: SerializeField]
	public Word targetWord { get; set; }
	[field: SerializeField]
	public Word magicWord { get; set; }
	[field: SerializeField]
	public E_SelectingType subjectSelectingType { get; set; }
	[field: SerializeField]
	public E_SelectingType targetSelectingType { get; set; }

	public SentenceUI ui => m_SentenceUI;

	public virtual bool isCompleted =>
		subjectWord != null &&
		targetWord != null &&
		magicWord != null;
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	protected static SentenceManager M_Sentence => SentenceManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (생성될 때)
	/// </summary>
	public override void Initialize()
	{
		//m_TargetSelectingType = (E_SelectingType)Random.Range((int)E_SelectingType.Closest, (int)E_SelectingType.Max);
		m_SentenceUI = GetComponent<SentenceUI>();

		m_WordTextMap = new Dictionary<string, System.Func<string>>();
		m_WordTextMap.Add("SubjectSelectingType", () => GetSelectingTypeToString(subjectSelectingType));
		m_WordTextMap.Add("TargetSelectingType", () => GetSelectingTypeToString(targetSelectingType));
	}
	/// <summary>
	/// 마무리화 함수 (파괴될 때)
	/// </summary>
	public override void Finallize()
	{
		m_WordTextMap.Clear();
		m_WordTextMap = null;

		m_SentenceUI = null;
	}

	/// <summary>
	/// 초기화 함수 (스폰될 때)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

		m_SentenceUI.Initialize();
	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{
		m_SentenceUI.Finallize();

		base.FinallizePoolItem();
	}

	//static Sentence()
	//{
	//	m_ToStringMap = new Dictionary<string, string>();
	//}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	protected virtual void ActivateSentence()
	{
		if (isCompleted == false)
			return;

		List<IWordObject> subjectList = M_Sentence.GetWordObjectList(subjectSelectingType, subjectWord);
		foreach (IWordObject subject in subjectList)
		{
			subject.ActivateSentence(targetSelectingType, targetWord, magicWord);
		}
	}

	public string GetWordText(string template)
	{
		if (m_WordTextMap.TryGetValue(template, out System.Func<string> textFunc) == false)
			return string.Empty;

		return textFunc();
	}
	protected string GetSelectingTypeToString(E_SelectingType selectingType)
	{
		string text = string.Empty;

		switch (selectingType)
		{
			case E_SelectingType.Nearest:
				text = "가장 가까운";
				break;
			case E_SelectingType.Random:
				text = "무작위";
				break;
			case E_SelectingType.Around:
				text = "주변 모든";
				break;
		}

		return text;
	}
}