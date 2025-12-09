using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectileMagic : Magic
{
	#region 기본 템플릿
	#region 변수
	protected string m_ProjectileKey = string.Empty;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	protected override void OnMagicActivating(IWordObject subject, IWordObject target)
	{
		Vector2 newPos = target.transform.position - subject.transform.position;
		float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;

		Projectile projectile = M_Projectile.GetBuilder(m_ProjectileKey)
			.SetPosition(subject.transform.position)
			.SetRotation(Quaternion.Euler(0, 0, rotZ))
			.SetActive(true)
			.Spawn();
		projectile.Initialize();
	}
	#endregion
	#endregion

	#region 매니저
	protected static ProjectileManager M_Projectile => ProjectileManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	#endregion
	#endregion
}