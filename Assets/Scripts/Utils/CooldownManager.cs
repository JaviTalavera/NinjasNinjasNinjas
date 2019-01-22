using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour {
	
	private Image m_skill1;
	private Text m_skillText1;
	private Image m_skill2;
	private Text m_skillText2;
	private Image m_skill3;
	private Text m_skillText3;
	private SkillHandler m_skillHandler;

	// Use this for initialization
	void Start () {
		m_skillHandler = GetComponent<SkillHandler> ();
		int id = m_skillHandler.m_PlayerStats.id;
		GameObject panel = GameObject.Find ("Skills" + id);
		m_skill1 = panel.transform.GetChild (0).GetChild (0).GetComponent<Image>();
		m_skillText1 = m_skill1.transform.GetChild (0).GetComponent<Text> ();
		m_skill2 = panel.transform.GetChild (1).GetChild (0).GetComponent<Image>();
		m_skillText2 = m_skill2.transform.GetChild (0).GetComponent<Text> ();
		m_skill3 = panel.transform.GetChild (2).GetChild (0).GetComponent<Image>();
		m_skillText3 = m_skill3.transform.GetChild (0).GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_skill1.fillAmount = m_skillHandler.GetCooldownTimePorcen (1);
		m_skillText1.text = m_skillHandler.GetCooldownTime (1).ToString("F1");
		m_skill2.fillAmount = m_skillHandler.GetCooldownTimePorcen (2);
		m_skillText2.text = m_skillHandler.GetCooldownTime (2).ToString("F1");
		m_skill3.fillAmount = m_skillHandler.GetCooldownTimePorcen (3);
		m_skillText3.text = m_skillHandler.GetCooldownTime (3).ToString("F1");
	}
}
