using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkill2 : SkillAuto {

    public static int[] municion = { 2, 2, 2, 2 };

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        if (ShadowSkill2.municion[owner - 1] > 0)
        {
            GameObject go = Instantiate(this.gameObject, new Vector3(position.x, 0.01f, position.z), this.transform.rotation);
            go.GetComponent<ShadowSkill2>().m_handler = handler;
            go.GetComponent<ShadowSkill2>().m_direccion = direccion;
            go.GetComponent<ShadowSkill2>().m_skillId = Random.Range(0, 10000);
            go.GetComponent<ShadowSkill2>().m_owner = owner;
            go.GetComponent<ShadowSkill2>().m_team = team;
            ShadowSkill2.municion[owner - 1]--;
            return go;
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerStats>().teamId != m_team)
        {
            BuffSlow buff = other.gameObject.AddComponent<BuffSlow>();
            buff.Init(100, 2f, m_skillId, m_handler.m_PlayerStats);
            GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject, 1f);
        }
    }
}
