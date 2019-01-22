using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour {
    public static int iNumPlayers = 0;
    public int m_internalId;
    public int m_playerId;
    public bool m_activo = false;
    public bool m_selected = false;
    public PlayerSelection m_selection;
    public Sprite m_basic;
    private int m_team = 1;

    // Use this for initialization
    void Start () {
        m_selection.SetPlayerHandler(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_selected && Input.GetButtonDown("Fire2" + m_internalId)) // DESELECCIONO PARA CAMBIAR DE PERSONAJE
        {
            Color c = m_selection.GetComponent<Image>().color;
            c.a = 0.5f;
            m_selection.GetComponent<Image>().color = c;
            m_selected = false;
            GameObject.Find("GameVars").GetComponent<GameVars>().players[m_internalId] = "X|"+ m_playerId+"|1";
        }
        else if (m_activo && Input.GetButtonDown("Fire2" + m_internalId)) // NO VOY A JUGAR ASÍ QUE ME QUITO
        {
            PlayerHandler.iNumPlayers--;
            m_selection.gameObject.SetActive(false);
            m_activo = false;
            GameObject.Find("GameVars").GetComponent<GameVars>().players[m_internalId] = "X|X|X";
            Image img = GameObject.Find("imagePlayer" + m_playerId).GetComponent<Image>();
            img.sprite = m_basic;
        }
        else if (!m_activo && Input.GetButtonDown("Fire1"+m_internalId)) // ME ACTIVO PARA PODER ELEGIR PERSONAJE Y JUGAR
        {
            m_activo = true;
            m_playerId = iNumPlayers + 1;
            PlayerHandler.iNumPlayers++;
            m_selection.gameObject.SetActive(true);
            m_team = m_playerId;
            GameObject.Find("imagePlayer" + m_playerId).GetComponentInChildren<Text>().text = "TEAM " + m_team;
            GameObject.Find("GameVars").GetComponent<GameVars>().players[m_internalId] = m_playerId+"|X|"+ m_team;
        }
        else if (m_activo && !m_selected && Input.GetButtonDown("Fire1" + m_internalId))
        {
            Color c = m_selection.GetComponent<Image>().color;
            c.a = 1f;
            m_selection.GetComponent<Image>().color = c;
            m_selected = true;
            GameObject.Find("GameVars").GetComponent<GameVars>().players[m_internalId] = m_playerId + "|"+ m_selection.m_character.m_ninja_id+"|" + m_team;
        }
        else if (m_selected && Input.GetButtonDown("Fire1" + m_internalId))
        {
			if (PlayerHandler.iNumPlayers > 1) {
				GameObject[] handlers = GameObject.FindGameObjectsWithTag ("Handler");
				List<int> teams = new List<int> ();
				foreach (GameObject go in handlers) {
					PlayerHandler p = go.GetComponent<PlayerHandler> ();
					if (p.m_activo) {
						if (p.m_selected) {
							if (!teams.Contains (p.m_team)) {
								teams.Add (p.m_team);
							}
						} else
							return;
					}
				}
				if (teams.Count > 1)
					GameObject.Find ("GameVars").GetComponent<GameVars> ().InitGame ();
			}
        }
        if (Input.GetButtonDown("Fire3" + m_internalId)) // CAMBIO DE EQUIPO
        {
			m_team = ((m_team)%PlayerHandler.iNumPlayers)+1;
            GameObject.Find("GameVars").GetComponent<GameVars>().players[m_internalId] = m_playerId + "|"+m_selection.m_character.m_ninja_id+"|" + m_team;

            GameObject.Find("imagePlayer" + m_playerId).GetComponentInChildren<Text>().text = "TEAM " + m_team;
        }
    }
}
