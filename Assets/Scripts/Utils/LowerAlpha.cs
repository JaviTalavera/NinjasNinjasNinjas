using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerAlpha : MonoBehaviour {
    Material m;
    // Use this for initialization
    void Start ()
    {
        m  = GetComponent<MeshRenderer>().materials[0];
    }
	
	// Update is called once per frame
	void Update () {
        if (m.color.a > 0)
        {
            Color c = m.color;
            c.a -= 0.3f * Time.deltaTime;
            m.color = c;
        }
        else if (m.color.a < 0)
            Destroy(gameObject);
	}
}
