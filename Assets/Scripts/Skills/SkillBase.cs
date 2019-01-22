using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour {

    public float m_multiplier; // Multiplicador de la fuerza del personaje
    public ParticleSystem m_colision_player;
    protected Vector3 m_direccion;
    protected int m_skillId;
    protected int m_owner;
    protected int m_team;
    protected SkillHandler m_handler;
    public ParticleSystem m_particle;

    public void StopMovimiento()
    {
        if (m_particle != null)
        {
            m_particle.transform.parent = null;
            m_particle.Stop();
            Destroy(m_particle.gameObject, 1.0f);
        }
        Destroy(gameObject);
    }

    public int GetOwner()
    {
        return m_owner;
    }

    public int GetTeam()
    {
        return m_team;
    }

    public void DoDamage(GameObject gameObject, Vector3 hit, float time2Damage = 0)
    {
        if (m_colision_player != null)
        {
            Vector3 pos;
            if (hit.y == 0) pos = new Vector3(hit.x, 0.4f, gameObject.transform.position.z -0.01f);
            else pos = new Vector3(hit.x, hit.y, gameObject.transform.position.z - 0.01f);
            Instantiate(m_colision_player, pos, Quaternion.identity);
        }
        gameObject.AddComponent<BuffDamageRetardado>();
        gameObject.GetComponent<BuffDamageRetardado>().Init((int)GetDamage(),time2Damage, m_skillId, m_handler.m_PlayerStats);
    }

    public float GetDamage ()
    {
        return m_multiplier * m_handler.GetComponent<PlayerStats>().strenght;
    }

    public void IgnoreCollision (bool bIgnore)
    {
        if (GetComponent<Collider>() != null)
            Physics.IgnoreCollision(m_handler.GetComponent<Collider>(), this.GetComponent<Collider>(), bIgnore);
    }

    public void IgnoreCollision(Collider collider, bool bIgnore)
    {
        if (GetComponent<Collider>() != null)
            Physics.IgnoreCollision(collider, this.GetComponent<Collider>(), bIgnore);
    }

    public virtual GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        GameObject go = GameObject.Instantiate(this.gameObject, new Vector3(position.x, 0.01f, position.z), Quaternion.identity);
        go.GetComponent<SkillBase>().m_handler = handler;
        go.GetComponent<SkillBase>().m_skillId = Random.Range(0, 10000);
        go.GetComponent<SkillBase>().m_owner = owner;
        go.GetComponent<SkillBase>().m_direccion = direccion;
        go.GetComponent<SkillBase>().m_team = team;
        return go;
    }


}
