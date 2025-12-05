using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum E_WordType
{
	// 플레이어
	Player,
	// 적
	Enemy,
	// 마법
	Magic,

	Max
}

public class Word : ObjectPoolItem<Word>
{
	#region 기본 템플릿
	#region 변수
	[SerializeField]
	protected E_WordType m_WordType;
	[SerializeField]
	protected string m_WordKey;
	#endregion

	#region 프로퍼티
	public E_WordType wordType => m_WordType;
	public string wordKey => m_WordKey;
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
	public virtual void Initialize()
	{

	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public virtual void Finallize()
	{

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


		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion
}