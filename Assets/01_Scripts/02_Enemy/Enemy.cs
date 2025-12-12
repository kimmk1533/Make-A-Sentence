using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Enemy : WordObject<Enemy, EnemyStat>, IDamageSender, IDamageReceiver
{
	#region 기본 템플릿
	#region 변수
	protected const float c_MinDamage = 0f;

	protected UtilClass.Timer m_AttackTimer = null;
	#endregion

	#region 프로퍼티
	public Stat stat => m_Stat;
	#endregion

	#region 이벤트
	public event System.Action onGiveDamage = null;
	public event System.Action onTakeDamage = null;
	public event System.Action onDeath = null;

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

		m_AttackTimer = new UtilClass.Timer();
		m_AttackTimer.Pause();
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
	/// </summary>
	public override void Finallize()
	{
		m_AttackTimer = null;

		m_Stat = null;

		base.Finallize();
	}

	/// <summary>
	/// 초기화 함수 (스폰될 때)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

		m_AttackTimer.interval = m_Stat.atkSpeed;
		m_AttackTimer.Resume();
	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{
		m_AttackTimer.Clear();
		m_AttackTimer.Pause();

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

		float speed = m_Stat.movingSpeed * Time.deltaTime;
		if (speed > Vector2.Distance(M_Player.GetPlayerPosition(), transform.position))
			speed = Vector2.Distance(M_Player.GetPlayerPosition(), transform.position);

		transform.position += (Vector3)movingDirection.normalized * speed;
	}

	public void GiveDamage(IDamageReceiver damageReceiver)
	{
		onGiveDamage?.Invoke();

		float damage = m_Stat.atkPower;
		damageReceiver.TakeDamage(this, damage);
	}
	public void TakeDamage(IDamageSender damageSender, float damage)
	{
		m_Stat.hp -= Mathf.Max(c_MinDamage, damage - m_Stat.defPower);
		m_Stat.hp = Mathf.Clamp(m_Stat.hp, 0f, m_Stat.maxHp);

		onTakeDamage?.Invoke();

		if (m_Stat.hp <= 0f)
		{
			Death();
		}
	}
	public void Death()
	{
		onDeath?.Invoke();

		M_Enemy.Despawn(this);
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
		if (string.IsNullOrEmpty(m_Stat.atkProjectile) == true)
			return;
		if (Physics2D.OverlapCircle(transform.position, m_Stat.nearbyRadius, LayerMask.GetMask("Player")) == null)
			return;

		Vector2 newPos = M_Player.GetPlayerPosition() - transform.position;
		float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;

		Projectile projectile = M_Projectile.GetBuilder(m_Stat.atkProjectile)
			.SetPosition(transform.position)
			.SetRotation(Quaternion.Euler(0, 0, rotZ))
			.SetActive(true)
			.Spawn();

		projectile.subject = this;
		projectile.target = M_Player.player;
		projectile.gameObject.layer = LayerMask.NameToLayer("EnemyMagic");
		projectile.onDespawn += (self) => { self.gameObject.layer = LayerMask.NameToLayer("PlayerMagic"); };
	}
}