using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill1 : SkillDirectional
{
    public float m_iceMultiplier;

    public void BuffSkill ()
    {
        this.m_distance = (int)(m_iceMultiplier*this.m_distance);
        this.target = transform.position+ m_direccion.normalized * m_distance;
        this.m_multiplier =(int)(m_iceMultiplier * this.m_multiplier);
        this.m_speed = (int)(m_iceMultiplier * this.m_speed);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && m_team != collision.gameObject.GetComponent<PlayerStats>().teamId)
        {
            DoDamage(collision.gameObject, collision.contacts[0].point, 0f);
            IgnoreCollision(collision.collider, true);
            StopMovimiento();
        }
        else if (collision.gameObject.tag == "IcePilar" && m_owner == collision.gameObject.GetComponent<SkillBase>().GetOwner())
        {
            Throw(collision.transform.position, new Vector3( 1f, 0,  0f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3( 1f, 0, -1f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3( 1f, 0,  1f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3( 0f, 0,  1f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3( 0f, 0, -1f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3(-1f, 0,  1f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3(-1f, 0, -1f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            Throw(collision.transform.position, new Vector3(-1f, 0,  0f), m_handler, m_owner, m_team).GetComponent<IceSkill1>().BuffSkill();
            StopMovimiento();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            StopMovimiento();
    }
}
