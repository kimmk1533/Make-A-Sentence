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
		Debug.Log("[" + subject.ToString() + "] Activate Heal Magic!");
		Debug.Log("Heal " + target.ToString());
	}
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	#endregion
	#endregion
}