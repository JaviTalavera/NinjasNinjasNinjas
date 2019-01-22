using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterCast : MonoBehaviour {

    public GameObject splat;

    public void Play() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), -Vector3.up, out hit, Mathf.Infinity, (1 << LayerMask.NameToLayer("World"))))
        {
            GameObject theSplat = Instantiate(splat, hit.point+new Vector3(0,0.01f,0), Quaternion.identity);
            Destroy(theSplat, 2);
        }
    }
}
