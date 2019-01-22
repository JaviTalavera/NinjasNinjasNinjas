using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowWinner : MonoBehaviour {

    public float time2wait;
	// Use this for initialization
	void Start () {
        GameObject gv = GameObject.Find("GameVars");
        if (gv != null)
        {
            GameObject.Find("tbWinner").GetComponent<Text>().text = "Equipo " + gv.GetComponent<GameVars>().teamWinner + "\n¡GANA!";
        }
    }
	
	// Update is called once per frame
	void Update () {
        time2wait -= Time.deltaTime;

		if (time2wait < 0 && Input.anyKeyDown)
        {
            SceneManager.LoadScene("0_Menu");
        }
	}
}
