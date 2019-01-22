using UnityEngine;
using System.Collections;

public class BuffDoT : MonoBehaviour {

	public void DamageOverTime(float amount, float tiempo, float maxTiempo) {
		Destroy (this, maxTiempo);
		StartCoroutine (Damage(amount, tiempo));
	}

	public IEnumerator Damage(float amount, float tiempo) {
		while (true) {
            gameObject.GetComponent<PlayerStats>().doDamage((int)amount);
			yield return new WaitForSeconds(tiempo);
		}
	}
}
