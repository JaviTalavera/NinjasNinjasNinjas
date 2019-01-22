using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffLightSkill3 : Buff {
    
    public ParticleSystem m_instance;

	// Use this for initialization
	void Awake () {
	}

    protected override IEnumerator Apply()
    {
        yield return new WaitForSeconds(this.secondsToWait);
        m_instance.transform.parent = null;
        m_instance.Stop();
        Destroy(m_instance, 0.5f);
        Destroy(this);
    }
}
