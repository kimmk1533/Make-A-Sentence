using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : WordObject<Player>
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트
	public event System.Action onGiveDamage = null;
	public event System.Action onTakeDamage = null;

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static PlayerManager M_Player => PlayerManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (복제될 때)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		//m_NearbyFinder.findingLayerMask = LayerMask.GetMask("Player", "Enemy", "PlayerMagic", "EnemyMagic");
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
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

	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{


		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	private void OnMove(InputValue inputValue)
	{
		movingDirection = inputValue.Get<Vector2>();
	}

	private void Update()
	{
		Move();
	}
	#endregion
	#endregion

	public void GiveDamage(float damage)
	{
		onGiveDamage?.Invoke();
	}
	public void TakeDamage(float damage)
	{
		onTakeDamage?.Invoke();
	}
}