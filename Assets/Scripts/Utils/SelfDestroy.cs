using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float timeToDie = 0.0f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDie);
	}
}
