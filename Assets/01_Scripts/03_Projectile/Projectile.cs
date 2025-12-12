using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Projectile : WordObject<Projectile, ProjectileStat>
{
	#region 기본 템플릿
	#region 변수
	protected UtilClass.Timer m_LifeTimeTimer = null;
	#endregion

	#region 프로퍼티
	public IWordObject subject { get; set; }
	public IWordObject target { get; set; }

	protected IDamageSender damageSender => subject as IDamageSender;
	protected IDamageReceiver damageReceiver => target as IDamageReceiver;
	#endregion

	#region 이벤트
	public event System.Action onHit = null;
	public event System.Action onTimeOut = null;

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static ProjectileManager M_Projectile => ProjectileManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (복제될 때)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		m_LifeTimeTimer = new UtilClass.Timer();
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
	/// </summary>
	public override void Finallize()
	{
		m_LifeTimeTimer = null;

		m_Stat = null;

		base.Finallize();
	}

	/// <summary>
	/// 초기화 함수 (스폰될 때)
	/// </summary>
	public override void InitializePoolItem()
	{
		base.InitializePoolItem();

		m_LifeTimeTimer.interval = m_Stat.lifeTime;
		m_LifeTimeTimer.Resume();
	}
	/// <summary>
	/// 마무리화 함수 (디스폰될 때)
	/// </summary>
	public override void FinallizePoolItem()
	{
		m_LifeTimeTimer.Clear();
		m_LifeTimeTimer.Pause();

		base.FinallizePoolItem();
	}
	#endregion

	#region 유니티 콜백 함수
	private void OnTriggerEnter2D(Collider2D collision)
	{
		IWordObject wordObject = collision.GetComponent<IWordObject>();
		if (wordObject == null)
			return;
		if (wordObject == subject)
			return;

		if (damageSender != null)
			damageSender.GiveDamage(wordObject as IDamageReceiver);

		onHit?.Invoke();

		M_Projectile.Despawn(this);
	}

	private void Update()
	{
		m_LifeTimeTimer.Update();
		if (m_LifeTimeTimer.TimeCheck(true))
		{
			onTimeOut?.Invoke();

			M_Projectile.Despawn(this);
			return;
		}

		Move();
	}
	#endregion
	#endregion

	protected override void Move()
	{
		float z = transform.rotation.eulerAngles.z;
		movingDirection = new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));

		transform.position += m_Stat.movingSpeed * Time.deltaTime * (Vector3)movingDirection.normalized;
	}
}