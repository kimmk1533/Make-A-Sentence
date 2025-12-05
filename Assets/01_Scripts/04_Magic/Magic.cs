using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Magic
{
	#region 기본 템플릿
	#region 변수
	protected IWordObject m_Subject = null;
	protected IWordObject m_Target = null;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트
	public event System.Action onMagicActivating = null;

	#region 이벤트 함수
	protected virtual void OnMagicActivating()
	{
		Debug.Log("Activate Magic");
	}
	#endregion
	#endregion

	#region 매니저
	protected static ProjectileManager M_Projectile => ProjectileManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public virtual void Initialize(IWordObject subject, IWordObject target)
	{
		m_Subject = subject;
		m_Target = target;
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public virtual void Finallize()
	{
		m_Subject = null;
		m_Target = null;
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	public virtual void Activate()
	{
		onMagicActivating?.Invoke();
	}
	protected virtual void CreateProjectile(string key)
	{
		Vector2 newPos = m_Target.transform.position - m_Subject.transform.position;
		float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;

		Projectile projectile = M_Projectile.GetBuilder(key)
			.SetParent(null)
			.SetPosition(m_Subject.transform.position)
			.SetRotation(Quaternion.Euler(0, 0, rotZ))
			.SetActive(true)
			.Spawn();
		projectile.Initialize();
	}
}