using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TakeDamageConditionSentence : Sentence
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
	#endregion

	#region 매니저
	private static PlayerManager M_Player => PlayerManager.Instance;
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();


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

		M_Player.onPlayerTakeDamageFromEnemy += ActivateSentence;
	}
	/// <summary>
	/// 마무리화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public override void FinallizePoolItem()
	{
		M_Player.onPlayerTakeDamageFromEnemy -= ActivateSentence;

		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	protected override void ActivateSentence()
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