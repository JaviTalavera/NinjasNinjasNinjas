using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    public Image m_healthBar;
    public Image m_healthBarBack;
    private PlayerStats m_playerStats;

	// Use this for initialization
	void Start () {
        m_playerStats = GetComponent<PlayerStats>();
        m_healthBar.color = m_playerStats.getColor();
        m_healthBarBack.color = m_playerStats.getColorBack();
    }
    
    public void Refresh()
    {
        m_healthBar.color = m_playerStats.getColor();
        m_healthBarBack.color = m_playerStats.getColorBack();
    }
    	
	// Update is called once per frame
	void Update () {
        m_healthBar.fillAmount = m_playerStats.getPorcenHealth();

    }


}
