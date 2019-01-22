using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInner : SkillBase {
    
    public float m_duration;
    public float m_tiempoEntreTicks;
    protected float m_tiempoRestante = 0.0f;


    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        GameObject go = Instantiate(this.gameObject, new Vector3(position.x + 1.08f, 0.01f, position.z), this.transform.rotation);
        go.transform.parent = handler.transform;
        go.GetComponent<SkillInner>().m_handler = handler;
        go.GetComponent<SkillInner>().m_direccion = direccion;
        go.GetComponent<SkillInner>().m_skillId = Random.Range(0, 10000);
        go.GetComponent<SkillInner>().m_owner = owner;
        go.GetComponent<SkillInner>().m_team = team;
        return go;
    }


}
