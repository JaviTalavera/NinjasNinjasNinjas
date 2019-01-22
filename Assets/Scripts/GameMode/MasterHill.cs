using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterHill : MonoBehaviour {

    public float m_speed;
    public float m_multiplier4Player;
    private int m_team = 0;
    private int m_newTeam = 0;
    private float m_porcen_team = 0; // valor del 0 al 100
    private bool m_conflict = false;
    private int m_players = 0;
    public Image m_image;
    private Color[] m_teamColors;
    private List<PlayerStats> m_playersInHill;
    private bool m_gameStarted = false;
    private int m_lastTeam = 0;
    
    public void Init()
    {
        m_playersInHill = new List<PlayerStats>();
        m_teamColors = GameObject.Find("InitGame").GetComponent<InitGame>().teamColors;
        m_gameStarted = true;
    }
    
	void Update ()
    {
        if (!m_gameStarted) return;
        if (m_porcen_team >= 100)
        {
            int ganador = m_team;
            if (ganador != 0)
            {
                GameObject go = GameObject.Find("GameVars");
                if (go != null)
                {
                    GameVars gv = go.GetComponent<GameVars>();
                    gv.teamWinner = ganador;
                }
                SceneManager.LoadScene("9_End_Game");
            }
        }
        if (!m_conflict)
        {
            if (m_team == 0 || (m_newTeam != 0 && m_newTeam != m_lastTeam))
            {
                m_porcen_team -= m_speed * 4 * Time.deltaTime;
                m_image.fillAmount = m_porcen_team / 100.0f;
            }
            else
            {
                m_porcen_team += (m_speed + m_multiplier4Player * (m_players - 1)) * Time.deltaTime;
                m_image.fillAmount = m_porcen_team / 100.0f;
            }
        }
        if (m_porcen_team > 100) m_porcen_team = 100;
        else if (m_porcen_team < 0)
        {
            m_porcen_team = 0;
            m_team = m_newTeam;
            if (m_team != 0) m_image.color = m_teamColors[m_team - 1];
            m_newTeam = 0;
        }

        int aux = 0;
        m_conflict = false;

        if (m_playersInHill.Count == 0)
        {
            //m_image.color = Color.white;
            m_team = 0;
        }
        else
        {
            foreach (PlayerStats player in m_playersInHill)
            {
                if (aux == 0)
                    aux = player.teamId;
                else
                    if (aux != player.teamId)
                    m_conflict = true;

            }
            if (!m_conflict)
            {
                if (aux != m_team)
                {
                    m_newTeam = aux;
                    if (m_newTeam == m_lastTeam)
                    {
                        m_team = m_newTeam;
                        m_newTeam = 0;
                    }
                }
            }
        }
        if (m_team != 0) m_lastTeam = m_team;

    }

    public void ComprobarPunto (PlayerStats ps)
    {
        if (m_playersInHill.Contains(ps))
            m_playersInHill.Remove(ps);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats ps = other.GetComponent<PlayerStats>();
            m_playersInHill.Add(ps);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerStats ps = other.GetComponent<PlayerStats>();
            m_playersInHill.Remove(ps);
        }
    }
}
