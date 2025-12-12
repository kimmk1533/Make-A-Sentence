using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Heal : Magic
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
		IDamageSender damageSender = subject.transform.GetComponent<IDamageSender>();
		IDamageReceiver damageReceiver = target.transform.GetComponent<IDamageReceiver>();

		damageReceiver.TakeDamage(damageSender, -5f);
		damageReceiver.stat.hp = Mathf.Min(damageReceiver.stat.hp + 5f, damageReceiver.stat.maxHp);
	}
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	#endregion
	#endregion
}