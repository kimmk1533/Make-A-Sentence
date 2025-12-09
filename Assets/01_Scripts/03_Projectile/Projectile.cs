using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Projectile : WordObject<Projectile>
{
	#region 기본 템플릿
	#region 변수
	protected UtilClass.Timer m_LifeTimeTimer = null;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

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

		//m_NearbyFinder.findingLayerMask = LayerMask.GetMask("Player", "Enemy", "PlayerMagic", "EnemyMagic");

		m_LifeTimeTimer = new UtilClass.Timer(1f);
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
	private void Update()
	{
		m_LifeTimeTimer.Update();
		if (m_LifeTimeTimer.TimeCheck(true))
		{
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

		transform.position += movingSpeed * Time.deltaTime * (Vector3)movingDirection.normalized;
	}
}