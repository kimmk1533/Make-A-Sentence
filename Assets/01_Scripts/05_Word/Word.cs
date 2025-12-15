using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum E_WordType
{
	// 플레이어
	Player = 1 << 0,
	// 적
	Enemy = 1 << 1,
	// 마법
	Magic = 1 << 2,
}

[RequireComponent(typeof(WordUI))]
public class Word : ObjectPoolItem<Word>
{
	#region 기본 템플릿
	#region 변수
	// Game

	// UI
	protected WordUI m_WordUI = null;
	#endregion

	#region 프로퍼티
	[field: SerializeField, ReadOnly]
	public E_WordType wordType { get; set; }
	[field: SerializeField, ReadOnly]
	public string magicTitle { get; set; }

	public WordUI ui => m_WordUI;
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (생성될 때)
	/// </summary>
	public override void Initialize()
	{
		m_WordUI = GetComponent<WordUI>();
	}
	/// <summary>
	/// 마무리화 함수 (파괴될 때)
	/// </summary>
	public override void Finallize()
	{
		m_WordUI = null;
	}

	/// <summary>
	/// 초기화 함수 (스폰될 때)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

		m_WordUI.Initialize();
	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{
		m_WordUI.Finallize();

		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion
}