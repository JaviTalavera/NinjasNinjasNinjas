using UnityEngine;
using System.Collections;

public class BuffHoT : Buff {
	public bool isOnArea = true;

	public void HealOverTime(float amount, float time, float maxTime) {
		Destroy (this, maxTime);
		StartCoroutine (Heal(amount, time));
	}

	public IEnumerator Heal(float amount, float time) {
		while (isOnArea) {
			gameObject.GetComponent<PlayerStats>().editHealth((int)-amount);
			yield return new WaitForSeconds(time);
		}
	}
}
