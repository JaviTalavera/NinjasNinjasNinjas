using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class InitGame : MonoBehaviour
{

    public Hashtable m_players;
    public Color[] teamColors;
    public Color[] teamColorsBack;
    public GAMEMODE m_mode;

    public enum GAMEMODE { DEATHMATCH, MASTERHILL };

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.Find("GameVars");
        if (go != null)
        {
            GameVars gv = go.GetComponent<GameVars>();
            gv.teamWinner = 0;
            m_players = gv.players;
            if (m_players.Count == 1)
                m_players[2] = "2|2|2";
        }
        else
        {
            //id jugador|tipo ninja|equipo
            m_players = new Hashtable();
            m_players[1] = "1|1|1";
            m_players[2] = "2|2|2";
            //m_players[3] = "3|3|1";
            //m_players[4] = "4|1|4";
        }


        IceSkill2.municion[0] = 5;
        IceSkill2.municion[1] = 5;
        IceSkill2.municion[2] = 5;
        IceSkill2.municion[3] = 5;

        FireSkill1.municion[0] = 3;
        FireSkill1.municion[1] = 3;
        FireSkill1.municion[2] = 3;
        FireSkill1.municion[3] = 3;

		ShadowSkill2.municion [0] = 2;
		ShadowSkill2.municion [1] = 2;
		ShadowSkill2.municion [2] = 2;
		ShadowSkill2.municion [3] = 2;

        foreach (int i in m_players.Keys)
        {
			int id = int.Parse(m_players[i].ToString().Split('|')[0]);
			int tipoNinja = int.Parse(m_players[i].ToString().Split('|')[1]);
			int idTeam = int.Parse(m_players[i].ToString().Split('|')[2]);

			go = Resources.Load ("Skills"+id) as GameObject;
			GameObject p = Instantiate (go);
			p.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform);
			p.GetComponent<RectTransform> ().anchoredPosition = Vector3.zero;
			p.name = "Skills" + id;

            Vector3 pos = GameObject.Find("SpawnPoint" + i).transform.position;
            go = Resources.Load("Ninja" + tipoNinja) as GameObject;
             p = Instantiate(go, pos, Quaternion.identity);
            PlayerStats ps = p.GetComponent<PlayerStats>();
            ps.id = id;
            ps.teamId = idTeam;
            ps.SetColors(teamColors[idTeam - 1], teamColorsBack[idTeam - 1]);

        }

        if (m_mode == GAMEMODE.DEATHMATCH)
        {
            DeathMatch sc = GameObject.Find("GameMode").GetComponent<DeathMatch>();
            //gameObject.AddComponent(Type.GetType("DeathMatch"));
            //DeathMatch sc = gameObject.GetComponent<DeathMatch>();
            //sc.m_canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
            //sc.scoreUI = Resources.Load("UI/TeamScore") as GameObject;
            sc.Init();
        }
        else if (m_mode == GAMEMODE.MASTERHILL)
        {
            MasterHill sc = GameObject.Find("GameMode").GetComponent<MasterHill>();
            sc.Init();
            //sc.m_canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
            //sc.scoreUI = Resources.Load("UI/TeamScore") as GameObject;
            //sc.Init();
        }
    }

    public bool PuedoRevivir(PlayerStats ps)
    {
        if (m_mode == GAMEMODE.DEATHMATCH)
        {
            //GameMechanics(teamId);
            DeathMatch dm = GameObject.Find("GameMode").GetComponent<DeathMatch>();
            dm.AddPuntuacion(ps.teamId);
            if (!dm.PuedoRevivir(ps.teamId))
                return false;
        }
        else if (m_mode == GAMEMODE.MASTERHILL)
        {
            //GameMechanics(teamId);
            MasterHill dm = GameObject.Find("GameMode").GetComponent<MasterHill>();
            dm.ComprobarPunto(ps);
        }
        return true;
    }
}
