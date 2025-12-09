using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NearbyFinder : SerializedMonoBehaviour
{
	#region 기본 템플릿
	#region 변수
	private CircleCollider2D m_Collider2D = null;

	[SerializeField]
	protected Dictionary<int, HashSet<IWordObject>> m_NearbyObjectMap = null;
	#endregion

	#region 프로퍼티
	protected new CircleCollider2D collider2D
	{
		get
		{
			if (m_Collider2D == null)
				m_Collider2D = GetComponent<CircleCollider2D>();

			return m_Collider2D;
		}
	}
	public LayerMask findingLayerMask { get; set; }
	public bool isTrigger
	{
		get => collider2D.isTrigger;
		set => collider2D.isTrigger = value;
	}
	public float radius
	{
		get => collider2D.radius;
		set => collider2D.radius = value;
	}
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public void Initialize()
	{
		m_NearbyObjectMap = new Dictionary<int, HashSet<IWordObject>>();
		for (int i = 0; i < 32; ++i)
		{
			if (string.IsNullOrEmpty(LayerMask.LayerToName(i)) == true)
				continue;

			int layer = 1 << i;
			if ((layer & findingLayerMask) != layer)
				continue;

			m_NearbyObjectMap.Add(i, new HashSet<IWordObject>());
		}

		isTrigger = true;
	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public void Finallize()
	{
		foreach (var item in m_NearbyObjectMap)
		{
			item.Value.Clear();
		}
		m_NearbyObjectMap.Clear();
		m_NearbyObjectMap = null;
	}
	#endregion

	#region 유니티 콜백 함수
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_NearbyObjectMap.TryGetValue(collision.gameObject.layer, out HashSet<IWordObject> hashSet) == false)
			return;

		IWordObject wordObject = collision.GetComponent<IWordObject>();
		if (wordObject == null)
			return;

		hashSet.Add(wordObject);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (m_NearbyObjectMap.TryGetValue(collision.gameObject.layer, out HashSet<IWordObject> hashSet) == false)
			return;

		IWordObject wordObject = collision.GetComponent<IWordObject>();
		if (wordObject == null)
			return;

		hashSet.Remove(wordObject);
	}
	#endregion
	#endregion

	public List<IWordObject> GetNearbyWordObjectList(int layer)
	{
		List<IWordObject> nearbyWordObjectList = new List<IWordObject>();
		if (m_NearbyObjectMap.TryGetValue(layer, out HashSet<IWordObject> hashSet) == false ||
			hashSet.Count == 0)
			return nearbyWordObjectList;

		foreach (var item in hashSet)
			nearbyWordObjectList.Add(item);

		nearbyWordObjectList.Sort((a, b) =>
		{
			return (Vector3.SqrMagnitude(a.transform.position - transform.position)).CompareTo(Vector3.SqrMagnitude(b.transform.position - transform.position));
		});

		return nearbyWordObjectList;
	}
}