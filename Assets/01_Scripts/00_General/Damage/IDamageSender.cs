using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IDamageSender
{
	public Stat stat { get; }

	public event System.Action onGiveDamage;

	public void GiveDamage(IDamageReceiver damageReceiver);
}