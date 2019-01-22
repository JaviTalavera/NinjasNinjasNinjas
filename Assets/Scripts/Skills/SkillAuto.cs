using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAuto : SkillBase {

    public bool m_directionOffset;
    public Vector3 m_positionOffset;

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        Vector3 pos = new Vector3();
        if (m_directionOffset)
            pos = new Vector3(position.x + direccion.x * m_positionOffset.x, m_positionOffset.y, position.z + direccion.z * m_positionOffset.z);
        else
            pos = new Vector3(position.x, m_positionOffset.y, position.z);

        GameObject go = Instantiate(this.gameObject, pos, this.transform.rotation);
        go.GetComponent<SkillAuto>().m_handler = handler;
        go.GetComponent<SkillAuto>().m_direccion = direccion;
        go.GetComponent<SkillAuto>().m_skillId = Random.Range(0, 10000);
        go.GetComponent<SkillAuto>().m_owner = owner;
        go.GetComponent<SkillAuto>().m_team = team;
        //go.GetComponent<SkillAuto>().IgnoreCollision(true);
        handler.gameObject.GetComponentInChildren<Animator>().SetTrigger("attack");
        return go;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().teamId != m_team)
        {
            DoDamage(collision.gameObject, collision.contacts[0].point, 0.5f);
        }
    }
}
