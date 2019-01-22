using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterCore : MonoBehaviour {

    public GameObject m_splat;
   

    private void Awake()
    {
        int x = 0;
        int drops = Random.Range(2, 4);

        while (x <= drops)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 10))
            {
                GameObject splatter = Instantiate(m_splat, hit.point+new Vector3(0,0.01f,0), Quaternion.FromToRotation(Vector3.up, hit.normal));

                float scaler = Random.value;
                Vector3 newScale = new Vector3(splatter.transform.localScale.x * scaler, splatter.transform.localScale.y, splatter.transform.localScale.z * scaler);
                splatter.transform.localScale = newScale;
                var rater = Random.Range(0, 359);
                splatter.transform.RotateAround(hit.point, hit.normal, rater);

                //Destroy(splatter, 5);
            }
            x++;
        }
    }
}
