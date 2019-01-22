using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMatch : MonoBehaviour {

    public Canvas m_canvas;
    private int m_nTeams = 0;
    public GameObject scoreUI;
    private List<GameObject> scoresUI;
    private List<int> scores;
    private InitGame m_initGame;
    private bool m_gameStarted = false;
    public int m_vidas;

    private void Update()
    {
        if (!m_gameStarted) return;
        int ganador = GetGanador();
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

    public void Init ()
    {
        scoresUI = new List<GameObject>();
        scores = new List<int>();
        m_initGame = GameObject.Find("InitGame").GetComponent<InitGame>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<int> equipos = new List<int>();
        foreach (GameObject go in players)
        {
            if (!equipos.Contains(go.GetComponent<PlayerStats>().teamId))
                equipos.Add(go.GetComponent<PlayerStats>().teamId);
        }
        m_nTeams = equipos.Count;

        for (int i = 0; i < m_nTeams; i++)
        {
            Vector2 pos = new Vector2(105f * i - 105f, Screen.height/2);
            GameObject go = Instantiate(scoreUI);
            go.GetComponentInChildren<Image>().color = m_initGame.teamColors[i];
            go.transform.SetParent(m_canvas.transform);
            go.transform.localPosition = pos;
            scoresUI.Add(go);
            scores.Add(m_vidas);
            Refresh(i+1);
        }
        m_gameStarted = true;
    }

    public bool PuedoRevivir (int team)
    {
        if (scores[team - 1] == 0)
            return false;
        return true;
    }

    public int GetGanador()
    {
        int ganador = 0;
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] > 0 && ganador == 0)
                ganador = i+1;
            else if (scores[i] > 0 && ganador != 0)
                return 0;
        }
        return ganador;
    }
	
    public void AddPuntuacion(int team)
    {
        scores[team - 1] = scores[team - 1] - 1;
        Refresh(team);
    }
    public void Refresh(int team)
    {
        scoresUI[team - 1].GetComponentInChildren<Text>().text = scores[team - 1] + "";
    }
}
