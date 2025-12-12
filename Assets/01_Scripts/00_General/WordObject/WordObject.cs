using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public abstract class WordObject<TItem, TStat> : ObjectPoolItem<TItem>, IWordObject where TItem : ObjectPoolItem<TItem> where TStat : Stat
{
	#region 기본 템플릿
	#region 변수
	[SerializeField]
	protected TStat m_Stat = null;

	[SerializeField, ChildComponent("Renderer")]
	protected SpriteRenderer m_Renderer = null;
	#endregion

	#region 프로퍼티
	public string wordKey => poolKey;

	protected Vector2 movingDirection { get; set; }
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static MagicManager M_Magic => MagicManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (복제될 때)
	/// </summary>
	public override void Initialize()
	{
	}
	/// <summary>
	/// 마무리화 함수 (메모리에서 정리될 때)
	/// </summary>
	public override void Finallize()
	{
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
	#endregion
	#endregion

	protected virtual void Move()
	{
		transform.position += m_Stat.movingSpeed * Time.deltaTime * (Vector3)movingDirection.normalized;
	}

	public List<IWordObject> GetNearbyWordObjectList(E_SelectingType selectingType, int layer)
	{
		List<Collider2D> exceptColliderList = new List<Collider2D>();
		exceptColliderList.Add(null);
		Collider2D collider2D = GetComponent<Collider2D>();
		if (collider2D != null)
			exceptColliderList.Add(collider2D);

		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_Stat.nearbyRadius, 1 << layer)
			.Except(exceptColliderList)
			.OrderBy(collider => Vector2.Distance(transform.position, collider.transform.position))
			.ToArray();

		List<IWordObject> nearbyObjectList = new List<IWordObject>();
		if (colliders.Length == 0)
			return nearbyObjectList;

		foreach (Collider2D collider2d in colliders)
		{
			IWordObject wordObject = collider2d.GetComponent<IWordObject>();

			if (wordObject == null)
			{
				Debug.Log("Gatcha");
				continue;
			}

			nearbyObjectList.Add(wordObject);
		}

		List<IWordObject> targetList = null;
		switch (selectingType)
		{
			case E_SelectingType.Closest:
				targetList = nearbyObjectList.GetRange(0, 1);
				break;
			case E_SelectingType.Random:
				int count = Random.Range(1, nearbyObjectList.Count);

				if (count != 1)
				{
					for (int i = 0; i < count; ++i)
					{
						int j = Random.Range(i + 1, nearbyObjectList.Count);

						IWordObject temp = nearbyObjectList[i];
						nearbyObjectList[i] = nearbyObjectList[j];
						nearbyObjectList[j] = temp;
					}
				}

				targetList = nearbyObjectList.GetRange(0, count);
				break;
			case E_SelectingType.Nearby:
				targetList = nearbyObjectList;
				break;
		}

		return targetList;
	}
	public void ActivateSentence(E_SelectingType selectingType, Word targetWord, Word magicWord)
	{
		string wordType = ((targetWord.wordType == E_WordType.Magic) ? "Player" : "") + targetWord.wordType.ToString();

		List<IWordObject> nearbyObjectList = new List<IWordObject>();
		nearbyObjectList.AddRange(GetNearbyWordObjectList(selectingType, LayerMask.NameToLayer(wordType)));

		foreach (IWordObject nearbyObject in nearbyObjectList)
		{
			M_Magic.ActivateMagic(magicWord.wordKey, this, nearbyObject);
		}
	}
}