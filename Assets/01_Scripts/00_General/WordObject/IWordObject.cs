using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IWordObject
{
	public Transform transform { get; }

	public string wordKey { get; }

	public List<IWordObject> GetNearbyWordObjectList(E_SelectingType selectingType, int layer);
	public void ActivateSentence(E_SelectingType selectingType, Word targetWord, Word magicWord);
}