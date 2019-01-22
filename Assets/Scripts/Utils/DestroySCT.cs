using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroySCT : MonoBehaviour {

    private Text m_text;
    // Use this for initialization
    void Start () {
        m_text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
        if (m_text.color.a <= 0)
            Destroy(gameObject);
	}
}
