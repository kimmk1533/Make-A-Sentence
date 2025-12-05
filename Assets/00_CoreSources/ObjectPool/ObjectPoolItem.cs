using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IPoolItem
{
	public void InitializePoolItem();
	public void FinallizePoolItem();
}

public abstract class ObjectPoolItem<TItem> : SerializedMonoBehaviour, IPoolItem where TItem : ObjectPoolItem<TItem>
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
	public string poolKey { get; set; }
	public bool isSpawning { get; private set; }
	#endregion

	#region 매니저
	#endregion

	#region 이벤트
	public event System.Action<TItem> onSpawn = null;
	public event System.Action<TItem> onDespawn = null;

	#region 이벤트 함수
	#endregion
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public virtual void InitializePoolItem()
	{
		isSpawning = true;

		onSpawn?.Invoke(this as TItem);
	}
	/// <summary>
	/// 마무리화 함수 (ObjectManager를 통해 스폰하면 자동으로 호출되므로 직접 호출 X)
	/// </summary>
	public virtual void FinallizePoolItem()
	{
		onSpawn = null;
		onDespawn = null;

		onDespawn?.Invoke(this as TItem);

		isSpawning = false;
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion
}