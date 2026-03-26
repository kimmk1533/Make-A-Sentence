using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IWordObject
{
	public Transform transform { get; }
	public GameObject gameObject { get; }

	public string wordKey { get; }

	public List<IWordObject> GetNearbyWordObjectList(E_SelectingType selectingType, int layerMask);
	public void ActivateSentence(E_SelectingType selectingType, Word targetWord, Word magicWord);
}