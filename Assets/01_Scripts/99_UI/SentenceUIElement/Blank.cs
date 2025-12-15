using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Blank : SentenceUIElement
{
	#region 기본 템플릿
	#region 변수
	private Sentence m_Sentence = null;
	#endregion

	#region 프로퍼티
	public E_WordType deployableType { get; set; }
	public string template { get; set; }

	public Sentence sentecne => m_Sentence;
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
		base.Initialize();


	}
	/// <summary>
	/// 마무리화 함수 (파괴될 때)
	/// </summary>
	public override void Finallize()
	{


		base.Finallize();
	}

	/// <summary>
	/// 초기화 함수 (스폰될 때)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

		m_Sentence = GetComponentInParent<Sentence>();
	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{
		m_Sentence = null;

		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	public bool CheckDeployable(E_WordType wordType)
	{
		return deployableType.HasFlag(wordType);
	}
}