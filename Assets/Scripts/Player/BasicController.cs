using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BasicController : MonoBehaviour {

    [SerializeField]
    private float m_WalkSpeed;

    
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;

    public Vector3 m_direccion;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float speed;
        GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = -transform.forward * m_Input.y + transform.right * m_Input.x;
        //desiredMove = Vector3.forward;
        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.y = desiredMove.y * speed;
        m_MoveDir.z = 0; 

        transform.Translate(m_MoveDir * Time.fixedDeltaTime);
		if (Math.Abs (transform.position.x) > 6.0f) {
			Vector3 newPos = new Vector3 (Math.Sign(transform.position.x)*6.0f, transform.position.y, transform.position.z);
			transform.position = newPos;
		}
		if (Math.Abs (transform.position.z) > 6.0f) {
			Vector3 newPos = new Vector3 (transform.position.x, transform.position.y, Math.Sign(transform.position.z)*6.0f);
			transform.position = newPos;		
		}
    }

    private void GetInput(out float speed)
    {
        // Read input
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal" + transform.parent.GetComponent<PlayerStats>().id);
        float vertical = CrossPlatformInputManager.GetAxis("Vertical" + transform.parent.GetComponent<PlayerStats>().id);

        speed = m_WalkSpeed ;
        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 0)
        {
            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
            m_direccion = new Vector3(Math.Sign(horizontal), 0f, Math.Sign(vertical));
        }
    }
}
