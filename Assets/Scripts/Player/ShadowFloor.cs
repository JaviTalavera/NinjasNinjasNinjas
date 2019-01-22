using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFloor : MonoBehaviour {

    private Transform m_parent;

	// Use this for initialization
	void Start () {
        m_parent = transform.parent;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;
        if (Physics.Raycast(m_parent.position, -Vector3.up, out hit, Mathf.Infinity , (1 << LayerMask.NameToLayer("World"))))
        {
			Vector3 newPos = new Vector3(hit.point.x, 0.01f, hit.point.z);
			transform.position = newPos;
        }
    }
}
