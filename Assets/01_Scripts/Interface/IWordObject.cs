using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IWordObject
{
	public Transform transform { get; }
	public float movingSpeed { get; set; }
	public float nearbyRadius { get; set; }
	public string wordKey { get; }

	public IWordObjectManager GetManager();
	public List<IWordObject> GetNearbyWordObjectList(int layer, string wordKey)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, nearbyRadius, 1 << layer)
			.OrderBy(collider => Vector2.Distance(transform.position, collider.transform.position))
			.ToArray();

		List<IWordObject> wordObjectList = new List<IWordObject>();

		foreach (Collider2D collider2d in colliders)
		{
			IWordObject wordObject = collider2d.GetComponent<IWordObject>();

			if (wordObject == null)
				continue;
			if (wordObject.wordKey.Equals(wordKey) == false)
				continue;

			wordObjectList.Add(wordObject);
		}

		return wordObjectList;
	}

	public void ActivateSentence(E_SelectingType selectingType, Word targetWord, Word magicWord)
	{
		string wordType = ((targetWord.wordType == E_WordType.Magic) ? "Player" : "") + targetWord.wordType.ToString();
		List<IWordObject> targetList = GetNearbyWordObjectList(LayerMask.NameToLayer(wordType), targetWord.wordKey);
		switch (selectingType)
		{
			case E_SelectingType.Closest:
				if (targetList.Count > 0)
					MagicManager.Instance.ActivateMagic(magicWord.wordKey, this, targetList[0]);
				return;
			case E_SelectingType.Random:
				break;
			case E_SelectingType.Nearby:
				break;
			case E_SelectingType.Max:
				break;
			default:
				break;
		}
		foreach (IWordObject target in targetList)
		{
			MagicManager.Instance.ActivateMagic(magicWord.wordKey, this, target);
		}
	}
}