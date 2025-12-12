using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class ProjectileStat : Stat
{
	// 생존 시간
	[field: SerializeField]
	public float lifeTime { get; set; }
}