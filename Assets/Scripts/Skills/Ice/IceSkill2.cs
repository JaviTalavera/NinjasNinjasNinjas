using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill2 : SkillAuto {

    public ParticleSystem m_spawn;
    public static int[] municion = { 5, 5, 5, 5 };

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStats>().teamId != m_team)
            DoDamage(collision.gameObject, collision.contacts[0].point ,0.5f);
        else if (collision.gameObject.tag == "IceBall" && m_owner == collision.gameObject.GetComponent<SkillBase>().GetOwner())
        {
            Instantiate(m_colision_player, new Vector3(transform.position.x, 0.4f, transform.position.z), this.transform.rotation);
            IceSkill2.municion[m_owner - 1]++;
            Destroy(gameObject);
        }
    }

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        if (IceSkill2.municion[owner - 1] > 0)
        {
            Vector3 pos = new Vector3();
            if (m_directionOffset)
                pos = new Vector3(position.x + direccion.x * m_positionOffset.x, m_positionOffset.y, position.z + direccion.z * m_positionOffset.z);
            else
                pos = new Vector3(position.x, m_positionOffset.y, position.z);
            Instantiate(m_spawn, pos, this.transform.rotation);
            GameObject go = Instantiate(this.gameObject, pos, this.transform.rotation);
            go.GetComponent<IceSkill2>().m_handler = handler;
            go.GetComponent<IceSkill2>().m_direccion = direccion;
            go.GetComponent<IceSkill2>().m_skillId = Random.Range(0, 10000);
            go.GetComponent<IceSkill2>().m_owner = owner;
            go.GetComponent<IceSkill2>().m_team = team;
            IceSkill2.municion[owner - 1]--;
            return go;
        }
        return null;
    }
}
