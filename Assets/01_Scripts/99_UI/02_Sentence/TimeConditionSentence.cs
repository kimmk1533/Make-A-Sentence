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
	private UtilClass.Timer m_Timer = null;
	[SerializeField]
	private float m_Interval;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		//m_Interval = c_IntervalArr[Random.Range(0, c_IntervalArr.Length)];
		m_Timer = new UtilClass.Timer(m_Interval);
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public override void Finallize()
	{
		m_Timer = null;

		base.Finallize();
	}

	/// <summary>
	/// 초기화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

	}
	/// <summary>
	/// 마무리화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public override void FinallizePoolItem()
	{
		m_Timer.Clear();

		base.FinallizePoolItem();
	}

	static TimeConditionSentence()
	{
		m_ToStringMap.Add("ko-kr", "{m_Subject}이/가 {m_Interval}초마다 {m_TargetSelectingType} {m_Target}에게 {m_Magic}을/를 사용합니다");
	}
	#endregion

	#region 유니티 콜백 함수
	private void Update()
	{
		if (false == isCompleted)
		{
			m_Timer.Clear();
			return;
		}

		m_Timer.Update();
		if (m_Timer.TimeCheck(true))
		{
			ActivateSentence();
		}
	}

	private void OnValidate()
	{
		if (m_Timer == null)
			return;

		m_Timer.interval = m_Interval;
	}
	#endregion
	#endregion

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder(m_ToStringMap["ko-kr"]);

		sb.Replace("{m_Subject}", m_SubjectWord.wordType.ToString());
		sb.Replace("{m_Interval}", m_Interval.ToString());
		sb.Replace("{TargetSelectingType}", m_TargetSelectingType.ToString());
		sb.Replace("{m_Target}", m_TargetWord.wordType.ToString());
		sb.Replace("{m_Magic}", m_MagicWord.wordType.ToString()).ToString();

		return sb.ToString();
	}
}