using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill3 : SkillInner
{
    public int m_ralentizacion;
    private List<PlayerStats> players;

    // Use this for initialization
    void Start () {
        players = new List<PlayerStats>();
	}

    void Update()
    {
		if (m_duration < 0 || m_handler.m_PlayerStats.isDead())
        {
            StopMovimiento();
        }
        if (m_tiempoRestante <= 0)
        {
            foreach (PlayerStats ps in players)
            {
                DoDamage(ps.gameObject, ps.transform.position, 0f);
            }
            m_tiempoRestante = m_tiempoEntreTicks;
        }
        m_tiempoRestante -= Time.deltaTime;
        m_duration -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerStats>().teamId != m_team)
        {
            players.Add(other.GetComponent<PlayerStats>());
            Globals.WriteLog("IceSkill3: Meto a " + other.GetComponent<PlayerStats>().id);
            BuffSlow bs = other.GetComponent<PlayerStats>().gameObject.AddComponent<BuffSlow>();
            bs.Init(m_ralentizacion, m_duration, m_skillId, m_handler.m_PlayerStats);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerStats>().teamId != m_team)
        {
            players.Remove(other.GetComponent<PlayerStats>());
            Globals.WriteLog("IceSkill3: Saco a " + other.GetComponent<PlayerStats>().id);
            BuffSlow[] buffs = other.GetComponents<BuffSlow>();
            foreach (BuffSlow b in buffs)
                if (b.id == m_skillId)
                {
                    b.Revert();
                    return;
                }
        }
    }



}
