using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IDamageReceiver
{
	public Stat stat { get; }

	public event System.Action onTakeDamage;
	public event System.Action onDeath;

	public void TakeDamage(IDamageSender damageSender, float damage);
	public void Death();
}