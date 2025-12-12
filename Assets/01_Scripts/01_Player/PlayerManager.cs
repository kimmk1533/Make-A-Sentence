using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : ObjectManager<PlayerManager, Player>
{
	#region 기본 템플릿
	#region 변수
	private Player m_Player = null;
	#endregion

	#region 프로퍼티
	public Player player => m_Player;
	#endregion

	#region 이벤트
	public event System.Action onPlayerGiveDamageToEnemy = null;
	public event System.Action onPlayerTakeDamageFromEnemy = null;

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 기본 초기화 함수 (Init Scene 진입 시, 즉 게임 실행 시 호출)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();


	}
	/// <summary>
	/// 기본 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{
		base.Finallize();


	}

	/// <summary>
	/// 메인 초기화 함수 (본인 Main Scene 진입 시 호출)
	/// </summary>
	public override void InitializeMain()
	{
		base.InitializeMain();

		m_Player = GetBuilder("Player")
			.SetParent(null)
			.SetPosition(Vector3.zero)
			.SetActive(true)
			.Spawn();
		m_Player.Initialize();

		m_Player.onGiveDamage += onPlayerGiveDamageToEnemy;
		m_Player.onTakeDamage += onPlayerTakeDamageFromEnemy;
	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{
		m_Player.onGiveDamage -= onPlayerGiveDamageToEnemy;
		m_Player.onTakeDamage -= onPlayerTakeDamageFromEnemy;

		Despawn(m_Player);
		m_Player = null;

		base.FinallizeMain();
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	public Vector3 GetPlayerPosition() => m_Player.transform.position;
}