using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : WordObject<Enemy>
{
	#region 기본 템플릿
	#region 변수
	protected string m_AttackProjectile = string.Empty;
	protected UtilClass.Timer m_AttackTimer = null;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static EnemyManager M_Enemy => EnemyManager.Instance;

	private static PlayerManager M_Player => PlayerManager.Instance;
	private static ProjectileManager M_Projectile => ProjectileManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (복제될 때)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		//m_NearbyFinder.findingLayerMask = LayerMask.GetMask("Player", "Enemy", "PlayerMagic", "EnemyMagic");

		m_AttackTimer = new UtilClass.Timer();
		m_AttackTimer.onTime += CreateProjectile;
		m_AttackTimer.Pause();
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
	/// </summary>
	public override void Finallize()
	{
		m_AttackTimer = null;

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
	private void Update()
	{
		Move();
		Attack();
	}
	#endregion
	#endregion

	protected override void Move()
	{
		movingDirection = M_Player.GetPlayerPosition() - transform.position;

		float speed = movingSpeed * Time.deltaTime;
		if (speed > Vector2.Distance(M_Player.GetPlayerPosition(), transform.position))
			speed = Vector2.Distance(M_Player.GetPlayerPosition(), transform.position);

		transform.position += (Vector3)movingDirection.normalized * speed;
	}

	public void GiveDamage(float damage)
	{

	}
	public void TakeDamage(float damage)
	{

	}
	private void Attack()
	{
		if (m_AttackTimer == null ||
			m_AttackTimer.isPaused)
			return;

		m_AttackTimer.Update();
		if (m_AttackTimer.TimeCheck(true) == true)
			CreateProjectile();
	}
	private void CreateProjectile()
	{
		Vector2 newPos = M_Player.GetPlayerPosition() - transform.position;
		float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;

		Projectile projectile = M_Projectile.GetBuilder(m_AttackProjectile)
			.SetParent(null)
			.SetPosition(transform.position)
			.SetRotation(Quaternion.Euler(0, 0, rotZ))
			.SetActive(true)
			.Spawn();

		projectile.gameObject.layer = LayerMask.NameToLayer("EnemyMagic");
		projectile.Initialize();
	}
}