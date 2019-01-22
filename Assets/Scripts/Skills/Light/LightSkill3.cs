using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSkill3 : SkillInner {

    public float m_LightMultiplier;

    // Use this for initialization
    void Start () {
        ParticleSystem m_instance = GameObject.Instantiate(m_particle) as ParticleSystem;
        m_instance.transform.parent = m_handler.transform;
        m_instance.transform.localPosition = Vector3.zero+new Vector3(0, 0.4f, 0);
        BuffLightSkill3 buff = m_handler.gameObject.AddComponent<BuffLightSkill3>();
        buff.Init(0, this.m_duration, this.m_skillId, this.m_handler.m_PlayerStats);
        buff.m_instance = m_instance;
        Destroy(gameObject);
    }
}
