using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkill3 : SkillDirectional
{
    public float m_distanceMultiplier;
    private Collider m_collider;

    private void Start()
    {
        m_collider = this.GetComponent<Collider>();
    } 

    // Update is called once per frame
    public override void Update()
    {
        if (m_movimiento)
        {
            m_particle.startSize += 5.0f * Time.deltaTime;
            m_particle.startSpeed += 5.0f * Time.deltaTime;
            m_multiplier += m_distanceMultiplier * Time.deltaTime;
            ((SphereCollider)m_collider).radius += 1.0f * Time.deltaTime;
            Vector3 dir = transform.position - target;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * m_speed);
            if (Vector3.Distance(transform.position, target) < 0.2f)
                StopMovimiento();
        }
    }
}
