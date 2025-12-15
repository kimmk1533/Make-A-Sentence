using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SentenceUIText : SentenceUIElement
{
	#region 기본 템플릿
	#region 변수
	private TextMeshProUGUI m_Text = null;
	#endregion

	#region 프로퍼티
	public string text { get => m_Text.text; set => m_Text.text = value; }
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

		m_Text = GetComponent<TextMeshProUGUI>();
	}
	/// <summary>
	/// 마무리화 함수 (파괴될 때)
	/// </summary>
	public override void Finallize()
	{
		m_Text = null;

		base.Finallize();
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
		m_Text.text = string.Empty;

		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion


}