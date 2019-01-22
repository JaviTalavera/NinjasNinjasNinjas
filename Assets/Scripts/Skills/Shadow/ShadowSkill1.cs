using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkill1 : SkillDirectional
{
    public float m_duration;
    public float m_time2tick;

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && m_team != collision.gameObject.GetComponent<PlayerStats>().teamId)
        {
            BuffDoT buff = collision.gameObject.AddComponent<BuffDoT>();
            if (collision.gameObject.GetComponent<BuffSlow>() != null)
                buff.DamageOverTime((int)(2 * m_multiplier * m_handler.m_PlayerStats.strenght), m_time2tick, m_duration);
            else
                buff.DamageOverTime((int)(m_multiplier * m_handler.m_PlayerStats.strenght), m_time2tick, m_duration);
            IgnoreCollision(collision.collider, true);
            StopMovimiento();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            StopMovimiento();
    }
}
