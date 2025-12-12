using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : WordObject<Player, PlayerStat>, IDamageSender, IDamageReceiver
{
	#region 기본 템플릿
	#region 변수
	protected const float c_MinDamage = 0f;

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
	private static PlayerManager M_Player => PlayerManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (복제될 때)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
	/// </summary>
	public override void Finallize()
	{
		m_Stat = null;

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

		Debug.Log("Player Death");
	}
}