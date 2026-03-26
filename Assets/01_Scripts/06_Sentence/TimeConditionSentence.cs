using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimeConditionSentence : Sentence
{
	#region 기본 템플릿
	#region 변수
	private static readonly float[] c_IntervalArr = { 0.1f, 0.5f, 1f, 5f, 10f };
	[SerializeField]
	private float m_Interval;
	#endregion

	#region 프로퍼티
	public float interval
	{
		get => m_Interval;
		set
		{
			M_Sentence.RemoveTimeSentence(m_Interval, ActivateSentence);
			m_Interval = value;
			M_Sentence.AddTimeSentence(m_Interval, ActivateSentence);
		}
	}
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static SentenceManager M_Sentence => SentenceManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		m_WordTextMap.Add("Interval", () => m_Interval.ToString());

		// 디버깅
		m_Interval = 1.5f;
		// 기존 코드
		//m_Interval = c_IntervalArr[Random.Range(0, c_IntervalArr.Length)];
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public override void Finallize()
	{


		base.Finallize();
	}

	/// <summary>
	/// 초기화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

		M_Sentence.AddTimeSentence(m_Interval, ActivateSentence);
	}
	/// <summary>
	/// 마무리화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public override void FinallizePoolItem()
	{
		M_Sentence.RemoveTimeSentence(m_Interval, ActivateSentence);

		base.FinallizePoolItem();
	}

	//static TimeConditionSentence()
	//{
	//	m_ToStringMap.Add("en-us", "{m_Subject} uses {m_Magic} on the {m_TargetSelectingType} {m_Target} every {m_Interval} seconds");
	//	m_ToStringMap.Add("ko-kr", "{m_Subject}이/가 {m_Interval}초마다 {m_TargetSelectingType} {m_Target}에게 {m_Magic}을/를 사용합니다");
	//}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	//public override string ToString()
	//{
	//	StringBuilder sb = new StringBuilder(m_ToStringMap["ko-kr"]);

	//	sb.Replace("{m_Subject}", m_SubjectWord.wordType.ToString());
	//	sb.Replace("{m_Interval}", m_Interval.ToString());
	//	sb.Replace("{m_TargetSelectingType}", m_TargetSelectingType.ToString());
	//	sb.Replace("{m_Target}", m_TargetWord.wordType.ToString());
	//	sb.Replace("{m_Magic}", m_MagicWord.wordType.ToString()).ToString();

	//	return sb.ToString();
	//}
}