using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class MagicManager : SerializedSingleton<MagicManager>
{
	#region 기본 템플릿
	#region 변수
	private Dictionary<string, Magic> m_MagicMap = null;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static PlayerManager M_Player => PlayerManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 기본 초기화 함수 (Init Scene 진입 시, 즉 게임 실행 시 호출)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		m_MagicMap = new Dictionary<string, Magic>();
	}
	/// <summary>
	/// 기본 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{
		base.Finallize();

		m_MagicMap = null;
	}

	/// <summary>
	/// 메인 초기화 함수 (본인 Main Scene 진입 시 호출)
	/// </summary>
	public override void InitializeMain()
	{
		base.InitializeMain();

		//m_MagicMap.Add("FireBall", new FireBall());
	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{
		base.FinallizeMain();

		m_MagicMap.Clear();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	public void ActivateMagic(string key, IWordObject subject, IWordObject target)
	{
		if (m_MagicMap.TryGetValue(key, out Magic magic) == true)
		{
			magic.Activate(subject, target);
			return;
		}

		Type type = Type.GetType(key);
		if (type == null)
			return;

		magic = (Magic)Activator.CreateInstance(type);
		m_MagicMap.Add(key, magic);
		magic.Activate(subject, target);
	}
}