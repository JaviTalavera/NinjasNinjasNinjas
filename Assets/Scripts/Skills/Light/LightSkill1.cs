using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class LightSkill1 : SkillDirectional
{
    public float m_porcenHeal;
    public float m_healDistance;
    private LineRenderer m_renderer;
    private FirstPersonController m_controller;

    private void Start()
    {
        m_controller = m_handler.GetComponent<FirstPersonController>();
        m_controller.enabled = false;
        m_renderer = GetComponent<LineRenderer>();
        m_renderer.SetPosition(0, new Vector3(m_handler.transform.position.x, 0.4f, m_handler.transform.position.z));
        m_renderer.SetPosition(1, new Vector3(m_handler.transform.position.x, 0.4f, m_handler.transform.position.z));
    }

    private void OnDestroy()
    {
        m_controller.enabled = true;
    }

    private void FixedUpdate()
    {
        m_renderer.SetPosition(1, new Vector3(m_particle.transform.position.x, 0.4f, m_particle.transform.position.z));
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && m_team != collision.gameObject.GetComponent<PlayerStats>().teamId)
        {
            DoDamage(collision.gameObject, collision.contacts[0].point, 0f);
            IgnoreCollision(collision.collider, true);
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject go in players)
            {
                PlayerStats ps = go.GetComponent<PlayerStats>();
                if (ps.teamId == this.m_team && ps.id != this.m_owner && Vector3.Distance(m_handler.transform.position, go.transform.position) < m_healDistance)
                {
                    BuffLightSkill2 buff = ps.GetComponent<BuffLightSkill2>();
                    if (buff != null)
                        ps.doHeal((int)(GetDamage() *buff.GetAmount() * m_porcenHeal / 100.0f));
                    else
                        ps.doHeal((int)(GetDamage() * m_porcenHeal / 100.0f));
                }
            }
        }
    }

    public GameObject Throw2(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        GameObject go = GameObject.Instantiate(this.gameObject, new Vector3(position.x, 0.4f, position.z), Quaternion.identity);
        go.GetComponent<LightSkill1>().target = new Vector3(position.x, 0.4f, position.z) + direccion.normalized * m_distance;
        go.GetComponent<LightSkill1>().m_direccion = direccion;
        go.GetComponent<LightSkill1>().m_skillId = Random.Range(0, 10000);
        go.GetComponent<LightSkill1>().m_owner = owner;
        go.GetComponent<LightSkill1>().m_team = team;
        go.GetComponent<LightSkill1>().m_handler = handler;
        go.GetComponent<LightSkill1>().IgnoreCollision(true);
        return go;
    }

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
        if (handler.GetComponent<BuffLightSkill3>() != null)
        {
            Throw2(position, new Vector3(1f, 0, 0f), handler, owner, team);
            Throw2(position, new Vector3(1f, 0, -1f), handler, owner, team);
            Throw2(position, new Vector3(1f, 0, 1f), handler, owner, team);
            Throw2(position, new Vector3(0f, 0, 1f), handler, owner, team);
            Throw2(position, new Vector3(0f, 0, -1f), handler, owner, team);
            Throw2(position, new Vector3(-1f, 0, 1f), handler, owner, team);
            Throw2(position, new Vector3(-1f, 0, -1f), handler, owner, team);
            Throw2(position, new Vector3(-1f, 0, 0f), handler, owner, team);
        }
        else
            Throw2(position, direccion, handler, owner, team);
        return null;
    }
}
