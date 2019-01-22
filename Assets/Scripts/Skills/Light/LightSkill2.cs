using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSkill2 : SkillAuto {
    public float m_duration;
    public float m_tiempoEntreTicks;
    private float m_tiempoRestante;
    public float m_lightMultiplier;
    private List<PlayerStats> players;

    // Use this for initialization
    void Start()
    {
        players = new List<PlayerStats>();
        m_tiempoRestante = m_tiempoEntreTicks;
    }

    void Update()
    {   
        if (m_duration < 0)
        {
            StopMovimiento();
        }
        if (m_tiempoRestante <= 0.0f)
        {
            foreach (PlayerStats ps in players)
            {
                DoDamage(ps.gameObject, ps.transform.position, 0.0f);
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
            Globals.WriteLog("BuffLightSkill2: Meto a " + other.GetComponent<PlayerStats>().id);
        }
        else if (other.tag == "Player" && other.GetComponent<PlayerStats>().teamId == m_team)
        {
            Globals.WriteLog("Me meto a mi mismo");
            BuffLightSkill2 bs = other.GetComponent<PlayerStats>().gameObject.AddComponent<BuffLightSkill2>();
            bs.Init(m_lightMultiplier, m_duration, m_skillId, m_handler.m_PlayerStats);
        }
    }

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        Vector3 pos = new Vector3();
        if (m_directionOffset)
            pos = new Vector3(position.x + direccion.x * m_positionOffset.x, m_positionOffset.y, position.z + direccion.z * m_positionOffset.z);
        else
            pos = new Vector3(position.x, m_positionOffset.y, position.z);

        GameObject go = Instantiate(this.gameObject, pos, this.transform.rotation);
        go.GetComponent<LightSkill2>().m_handler = handler;
        go.GetComponent<LightSkill2>().m_direccion = direccion;
        go.GetComponent<LightSkill2>().m_skillId = Random.Range(0, 10000);
        go.GetComponent<LightSkill2>().m_owner = owner;
        go.GetComponent<LightSkill2>().m_team = team;
        handler.gameObject.GetComponentInChildren<Animator>().SetTrigger("attack");

        if (handler.GetComponent<BuffLightSkill3>() != null)
        {
            go.GetComponent<LightSkill2>().m_particle.startSize *= m_lightMultiplier;
            go.GetComponent<LightSkill2>().m_particle.transform.GetChild(0).GetComponent<ParticleSystem>().startSize *= m_lightMultiplier;
            ParticleSystem.ShapeModule shapeModule = go.GetComponent<LightSkill2>().m_particle.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
            shapeModule.radius *= m_lightMultiplier;
            go.GetComponent<LightSkill2>().m_particle.transform.GetChild(1).GetComponent<ParticleSystem>().startSize *= m_lightMultiplier;
            shapeModule = go.GetComponent<LightSkill2>().m_particle.transform.GetChild(1).GetComponent<ParticleSystem>().shape;
            shapeModule.radius *= m_lightMultiplier;
            go.GetComponent<LightSkill2>().m_multiplier *= m_lightMultiplier;
        }
            return go;
    }
    

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerStats>().teamId != m_team)
        {
            players.Remove(other.GetComponent<PlayerStats>());
            Globals.WriteLog("BuffLightSkill2: Saco a " + other.GetComponent<PlayerStats>().id);

        }
        else if (other.tag == "Player" && other.GetComponent<PlayerStats>().teamId == m_team)
        {
            BuffLightSkill2[] buffs = other.GetComponents<BuffLightSkill2>();
            foreach (BuffLightSkill2 b in buffs)
                if (b.id == m_skillId)
                {
                    b.Revert();
                    return;
                }
        }
    }
}
