using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSlow : Buff
{
    private PlayerStats m_playerStats;
    private int m_speed;
    private SpriteRenderer m_ninja;


    override public void Init(float amount, float duration, int id, PlayerStats owner)
    {
        this.id = id;
        this.amount = amount;
        this.secondsToWait = duration;
        this.owner = owner;

        m_playerStats = this.GetComponent<PlayerStats>();
        m_ninja = m_playerStats.transform.Find("NinjaSprite").GetComponent<SpriteRenderer>();
        m_speed = m_playerStats.speed - (int)(m_playerStats.speed * ((100.0f - amount) / 100.0f));

        m_playerStats.speed = m_playerStats.speed - m_speed;
        m_ninja.color = Color.blue;
        StartCoroutine("Apply");
    }

    override protected IEnumerator Apply()
    {
        yield return new WaitForSeconds(this.secondsToWait);
        m_playerStats.speed += m_speed;
        m_ninja.color = Color.white;
        Destroy(this);
    }

    override public void Revert()
    {
        m_playerStats.speed += m_speed;
        m_ninja.color = Color.white;
        Destroy(this);
    }
}
