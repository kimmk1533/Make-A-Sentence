using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum E_SelectingType
{
	// 가장 가까운 (1)
	Closest,
	// 랜덤 (1 ~ all)
	Random,
	// 주변 (all)
	Nearby,

	Max
}

public abstract class Sentence : ObjectPoolItem<Sentence>
{
	#region 기본 템플릿
	#region 변수
	protected static readonly Dictionary<string, string> m_ToStringMap = null;

	[SerializeField]
	protected Word m_SubjectWord = null;
	[SerializeField]
	protected Word m_TargetWord = null;
	[SerializeField]
	protected Word m_MagicWord = null;

	[SerializeField]
	protected E_SelectingType m_SubjectSelectingType;
	[SerializeField]
	protected E_SelectingType m_TargetSelectingType;
	#endregion

	#region 프로퍼티
	public virtual bool isCompleted =>
		m_SubjectWord != null &&
		m_TargetWord != null &&
		m_MagicWord != null;
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
	/// 초기화 함수 (복제될 때)
	/// </summary>
	public override void Initialize()
	{
		//m_TargetSelectingType = (E_SelectingType)Random.Range((int)E_SelectingType.Closest, (int)E_SelectingType.Max);
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
	/// </summary>
	public override void Finallize()
	{

	}

	/// <summary>
	/// 초기화 함수 (스폰될 때)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{


		base.FinallizePoolItem();
	}

	static Sentence()
	{
		m_ToStringMap = new Dictionary<string, string>();
	}
	#endregion

	#region 유니티 콜백 함수
	private void Awake()
	{
		InitializePoolItem();
		Initialize();
	}
	#endregion
	#endregion

	protected virtual void ActivateSentence()
	{
		if (isCompleted == false)
			return;

		List<IWordObject> subjectList = M_Sentence.GetWordObjectList(m_SubjectSelectingType, m_SubjectWord);
		foreach (IWordObject subject in subjectList)
		{
			subject.ActivateSentence(m_TargetSelectingType, m_TargetWord, m_MagicWord);
		}
	}
}