using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Projectile : ObjectPoolItem<Projectile>, IWordObject
{
	#region 기본 템플릿
	#region 변수
	protected UtilClass.Timer m_LifeTimeTimer = null;
	#endregion

	#region 프로퍼티
	[field: SerializeField]
	public float movingSpeed { get; set; }
	[field: SerializeField]
	public float nearbyRadius { get; set; }
	public string wordKey => poolKey;
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
	/// 초기화 함수
	/// </summary>
	public void Initialize()
	{
		m_LifeTimeTimer = new UtilClass.Timer(1f);
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public void Finallize()
	{

	}
	#endregion

	#region 유니티 콜백 함수
	private void Update()
	{
		Move();
	}
	#endregion
	#endregion

	public IWordObjectManager GetManager() => M_Projectile;

	private void Move()
	{
		float z = transform.rotation.eulerAngles.z;
		Vector2 direction = new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
		transform.position += (Vector3)direction * movingSpeed * Time.deltaTime;

		m_LifeTimeTimer.Update();
		if (m_LifeTimeTimer.TimeCheck(true))
		{
			M_Projectile.Despawn(this);
		}
	}
}