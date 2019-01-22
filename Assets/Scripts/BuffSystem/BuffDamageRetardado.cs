using UnityEngine;
using System.Collections;

public class BuffDamageRetardado : Buff {

	override protected IEnumerator Apply() {
		yield return new WaitForSeconds(this.secondsToWait);

        if (gameObject.tag == "Player")
			gameObject.GetComponent<PlayerStats> ().doDamage((int)amount);
    }
}
