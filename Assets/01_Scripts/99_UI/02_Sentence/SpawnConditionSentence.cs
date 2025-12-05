using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnConditionSentence : Sentence
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static EnemyManager M_Enemy => EnemyManager.Instance;
	private static ProjectileManager M_Projectile => ProjectileManager.Instance;
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

		switch (m_SubjectWord.wordType)
		{
			case E_WordType.Enemy:
				Debug.Log("Enemy");
				M_Enemy.onSpawn += ActivateSentence;
				break;
			case E_WordType.Magic:
				Debug.Log("Magic");
				M_Projectile.onSpawn += ActivateSentence;
				break;
		}
	}
	/// <summary>
	/// 마무리화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public override void FinallizePoolItem()
	{
		switch (m_SubjectWord.wordType)
		{
			case E_WordType.Enemy:
				M_Enemy.onSpawn -= ActivateSentence;
				break;
			case E_WordType.Magic:
				M_Projectile.onSpawn -= ActivateSentence;
				break;
		}

		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	private void ActivateSentence<TItem>(ObjectPoolItem<TItem> poolItem) where TItem : ObjectPoolItem<TItem> => ActivateSentence();
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