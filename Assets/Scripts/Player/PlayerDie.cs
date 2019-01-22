using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerDie : MonoBehaviour {
    
    public ParticleSystem m_animacion_muerte;
    private PlayerStats m_playerStats;
    private FirstPersonController m_controller;
    private SkillHandler m_skillHandler;
    private Collider m_collider;
    private bool m_reseting = false;
    public bool m_mostrarRespawn;
	// Use this for initialization
	void Start () {
        m_playerStats = GetComponent<PlayerStats>();
        m_controller = GetComponent<FirstPersonController>();
        m_skillHandler = GetComponent<SkillHandler>();
        m_collider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (!m_reseting && m_playerStats.getPorcenHealth() == 0)
        {
            Instantiate(m_animacion_muerte, transform.position, m_animacion_muerte.transform.rotation);
            m_playerStats.transform.GetComponentInChildren<Animator>().SetTrigger("die");
            m_controller.enabled = false;
            m_skillHandler.enabled = false;
            m_collider.enabled = false;
            m_reseting = true;

            Buff[] buffs = GetComponents<Buff>();
            foreach (Buff b in buffs)
                b.Revert();

            FireSkill1.municion[m_playerStats.id - 1] = 3;
            IceSkill2.municion[m_playerStats.id - 1] = 5;
			ShadowSkill2.municion[m_playerStats.id - 1] = 2;

            if (GameObject.Find("InitGame").GetComponent<InitGame>().PuedoRevivir(m_playerStats))
                StartCoroutine("Resurrection");
        }

        if (m_mostrarRespawn)
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            float minDistance = 1000f;
            Transform minPosition = null;
            Transform betterPosition = null;
            float maxMinDistance = 0f;
            foreach (GameObject sp in spawnPoints)
            {
                minDistance = 1000f;
                foreach (GameObject pl in players)
                {
                    if (pl.GetComponent<PlayerStats>().id != m_playerStats.id)
                    {
                        if (Vector3.Distance(pl.transform.position, sp.transform.position) < minDistance)
                        {
                            minPosition = sp.transform;
                            minDistance = Vector3.Distance(pl.transform.position, sp.transform.position);
                        }
                    }
                }
                if (minDistance > maxMinDistance)
                {

                    betterPosition = minPosition;
                    maxMinDistance = minDistance;
                }
            }
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, betterPosition.position);
        }
        else
        {
            GetComponent<LineRenderer>().enabled = false;
        }

    }

    public IEnumerator Resurrection ()
    {
        yield return new WaitForSeconds(3f);
        
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = 1000f;
        Transform minPosition = null;
        Transform betterPosition = null;
        float maxMinDistance = 0f;
        foreach (GameObject sp in spawnPoints)
        {
            minDistance = 1000f;
            foreach (GameObject pl in players)
            {
                if (pl.GetComponent<PlayerStats>().id != m_playerStats.id)
                {
                    if (Vector3.Distance(pl.transform.position, sp.transform.position) < minDistance)
                    {
                        minPosition = sp.transform;
                        minDistance = Vector3.Distance(pl.transform.position, sp.transform.position);
                    }
                }
            }
            if (minDistance > maxMinDistance)
            {

                betterPosition = minPosition;
                maxMinDistance = minDistance;
            }
        }
        m_playerStats.transform.GetComponentInChildren<Animator>().SetTrigger("revive");
        transform.position = betterPosition.position;
        m_playerStats.Reset();
        m_controller.enabled = true;
        m_skillHandler.enabled = true;
        m_collider.enabled = true;
        m_reseting = false;
    }
}
