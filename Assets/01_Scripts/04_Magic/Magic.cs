using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Magic
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트
	public event System.Action<IWordObject, IWordObject> onMagicActivating = null;

	#region 이벤트 함수
	protected abstract void OnMagicActivating(IWordObject subject, IWordObject target);
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	public Magic()
	{
		onMagicActivating += OnMagicActivating;
	}
	#endregion
	#endregion

	public virtual void Activate(IWordObject subject, IWordObject target)
	{
		onMagicActivating?.Invoke(subject, target);
	}
}