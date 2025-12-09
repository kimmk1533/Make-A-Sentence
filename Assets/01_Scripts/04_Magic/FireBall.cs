using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FireBall : ProjectileMagic
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	protected override void OnMagicActivating(IWordObject subject, IWordObject target)
	{
		base.OnMagicActivating(subject, target);
	}
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	public FireBall()
	{
		m_ProjectileKey = "FireBall";
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion
}