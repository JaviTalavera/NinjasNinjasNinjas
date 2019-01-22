using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FireSkill3 : SkillAuto {

    public ParticleSystem m_desaparecer;
    public GameObject m_explosion;
    public ParticleSystem m_fire;
    private ParticleSystem m_sello;
    private Vector3 m_oldPosition;
    
    public FirstPersonController m_controller;
    public BasicController m_sombra_controller;

	// Use this for initialization
	void Start () {
        m_sello = GetComponentInChildren<ParticleSystem>();
        m_controller = m_handler.gameObject.GetComponent<FirstPersonController>();
        m_controller.enabled = false;
        m_sombra_controller = m_handler.gameObject.GetComponentInChildren<BasicController>();
        m_sombra_controller.enabled = true;
        m_oldPosition = m_sombra_controller.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		int id = m_handler.GetComponent<PlayerStats> ().id;
		if (Input.GetButtonUp("Fire3"+m_handler.m_PlayerStats.id))
        {
            Vector3 oldPos = m_controller.transform.position;
            Vector3 newPos = m_sombra_controller.transform.position;
            GameObject.Instantiate(m_desaparecer, new Vector3(oldPos.x, 0.5f, oldPos.z), m_desaparecer.transform.rotation);
            m_controller.transform.position = newPos;
            m_controller.enabled = true;
            m_sombra_controller.enabled = false;
            m_sombra_controller.transform.localPosition = new Vector3(0,0.01f, 0);
            m_sello.transform.parent = null;
            m_sello.Stop();
            GameObject go = Instantiate(m_explosion, new Vector3(newPos.x, 0.01f, newPos.z), m_explosion.transform.rotation);
            go.GetComponent<FireSkill3Handle>().Init(m_multiplier, m_handler.m_PlayerStats.strenght, m_owner, m_team);
            Destroy(gameObject);
        }
        Color c = Color.black;
        c.a = 0.9f;
        DrawLine(m_oldPosition, m_sombra_controller.transform.position, c, 1f);
        m_oldPosition = m_sombra_controller.transform.position;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Standard"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
        GameObject.Instantiate(m_fire, end, m_desaparecer.transform.rotation);
    }
}
