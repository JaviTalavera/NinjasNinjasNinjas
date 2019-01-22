using UnityEngine;
using System.Collections;

public class BuffHealthModifier : Buff {

	public override void Init (float amount, float duration, int id, PlayerStats owner) {
		this.id = id;
		this.amount = amount;
		this.secondsToWait = duration;
		this.owner = owner;
		if (gameObject.tag == "Player")
            gameObject.GetComponent<PlayerStats>().doDamage((int)amount);
        if (gameObject.tag == "Enemy")
            gameObject.GetComponent<PlayerStats>().doDamage((int)-amount);
        Destroy(this);
    }
}
