using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class SkillDirectional : SkillBase {

    public int m_distance;
    public float m_speed;

    protected Vector3 target;
    protected bool m_movimiento = true;

    void Start () {
		
	}
	
	public virtual void Update () {
        if (m_movimiento)
        {
            Vector3 dir = transform.position - target;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * m_speed);
            if (Vector3.Distance(transform.position, target) < 0.2f)
            {
                StopMovimiento();
            }
        }
    }

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        GameObject go = GameObject.Instantiate(this.gameObject, new Vector3(position.x, 0.4f, position.z), Quaternion.identity);
        go.GetComponent<SkillDirectional>().target = new Vector3(position.x, 0.4f, position.z) + direccion.normalized * m_distance;
        go.GetComponent<SkillDirectional>().m_direccion = direccion;
        go.GetComponent<SkillDirectional>().m_skillId = Random.Range(0, 10000);
        go.GetComponent<SkillDirectional>().m_owner = owner;
        go.GetComponent<SkillDirectional>().m_team = team;
        go.GetComponent<SkillDirectional>().m_handler = handler;
        go.GetComponent<SkillDirectional>().IgnoreCollision(true);
        return go;
    }

    virtual public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && m_team != collision.gameObject.GetComponent<PlayerStats>().teamId)
        {
            DoDamage(collision.gameObject, collision.contacts[0].point, 0f);
            IgnoreCollision(collision.collider, true);
            StopMovimiento();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            StopMovimiento();
    }
}
