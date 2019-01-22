using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour {

    public float angle;
    private bool m_rotate = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_rotate)
            transform.Rotate(Vector3.forward, angle);
	}

    public void RotateStop()
    {
        m_rotate = false;
    }
}
