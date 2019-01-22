using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SkillHandler : MonoBehaviour {
    
    public SkillBase[] skills;
    public float[] cdSkills;
    public float[] timeToSkills;
    public float gcd;
    private float timeToGcd;
    //public int municion;
    public Text lblMunicion;
    public Text lblCd;
    private FirstPersonController m_fpc;
    public ParticleSystem efectoMunicion;
    public PlayerStats m_PlayerStats;


    // Use this for initialization
    void Start () {
        m_fpc = GetComponent<FirstPersonController>();
        m_PlayerStats = GetComponent<PlayerStats>();
        timeToGcd = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K))
            m_PlayerStats.doDamage(1000);

        if (timeToGcd <= 0 && Input.GetButtonDown("Fire1"+m_PlayerStats.id) && timeToSkills[0] <= 0)
        {
            skills[0].Throw(transform.position, m_fpc.m_direccion, this, m_PlayerStats.id, m_PlayerStats.teamId);
            timeToSkills[0] = cdSkills[0];
            timeToGcd = gcd;
        }
        else if (timeToGcd <= 0 && Input.GetButtonDown("Fire2" + m_PlayerStats.id) && timeToSkills[1] <= 0)
        {
            skills[1].Throw(transform.position, m_fpc.m_direccion, this, m_PlayerStats.id, m_PlayerStats.teamId);
            //municion--;
            timeToSkills[1] = cdSkills[1];
            timeToGcd = gcd;
        }
        else if (timeToGcd <= 0 && Input.GetButtonDown("Fire3" + m_PlayerStats.id) && timeToSkills[2] <= 0)
        {
            skills[2].Throw(transform.position, m_fpc.m_direccion, this, m_PlayerStats.id, m_PlayerStats.teamId);
            //municion--;
            timeToSkills[2] = cdSkills[2];
            timeToGcd = gcd;
        }
        for (int i = 0; i < skills.Length; i++)
        {
            timeToSkills[i] -= Time.deltaTime;
            if (timeToSkills[i] < 0)
                timeToSkills[i] = 0;
        }
        timeToGcd -= Time.deltaTime;
        if (timeToGcd < 0)
            timeToGcd = 0;
        // ACTUALIZAR UI
        //lblMunicion.text = "Munición: " + SkillNinja.municion;
        //lblCd.text = "Cd: " + timeToSkills[0].ToString("F2");
    }

	public float GetCooldownTime (int skill) {
		if (timeToSkills[skill-1] != 0)
			return timeToSkills[skill-1];
		else
			return timeToGcd;
	}


	public float GetCooldownTimePorcen (int skill) {
		if (timeToSkills[skill-1] != 0)
			return timeToSkills[skill-1] / cdSkills[skill-1];
		else
			return timeToGcd/gcd;
	}

    public void AddMunicion()
    {
        efectoMunicion.Play();
    }
}
