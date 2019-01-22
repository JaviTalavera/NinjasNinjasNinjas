using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill3Handle : MonoBehaviour {

    public float m_multiplier;
    public int m_owner;
    public int m_team;
    public int m_strenght;
    public ParticleSystem m_particle;

    public void Init(float multiplier, int strength, int owner, int team)
    {
        m_multiplier = multiplier;
        m_strenght = strength;
        m_owner = owner;
        m_team = team;
        m_particle.transform.SetParent(null);
        Destroy(gameObject, 0.25f);
    } 

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && m_team != other.GetComponent<PlayerStats>().teamId)
        {
            other.GetComponent<PlayerStats>().doDamage((int)(m_strenght * m_multiplier));
            Physics.IgnoreCollision(other, this.GetComponent < Collider >());
        }
    }
}
