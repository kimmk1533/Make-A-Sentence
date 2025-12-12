using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class PlayerStat : Stat
{
	// 공격력
	[field: SerializeField]
	public float atkPower { get; set; }
	// 방어력
	[field: SerializeField]
	public float defPower { get; set; }

	// 최대 생성 가능한 투사체 수
	[field: SerializeField]
	public int maxProjectile { get; set; }
}