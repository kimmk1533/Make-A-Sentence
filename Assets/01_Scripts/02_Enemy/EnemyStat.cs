using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class EnemyStat : Stat
{
	// 공격력
	[field: SerializeField]
	public float atkPower { get; set; }
	// 방어력
	[field: SerializeField]
	public float defPower { get; set; }

	// 공격 투사체
	[field: SerializeField]
	public string atkProjectile { get; set; }
	// 공격 속도
	[field: SerializeField]
	public float atkSpeed { get; set; }
}