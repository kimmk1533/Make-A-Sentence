using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Stat
{
	// 명칭
	[field: SerializeField]
	public string title { get; set; }

	// 최대 체력
	[field: SerializeField]
	public float maxHp { get; set; }
	// 체력
	[field: SerializeField]
	public float hp { get; set; }

	// 이동 속도
	[field: SerializeField]
	public float movingSpeed { get; set; }

	// 주변 감지 범위
	[field: SerializeField]
	public float nearbyRadius { get; set; }
}