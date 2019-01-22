using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public float maxHealth;
    public float health;
    private int maxSpeed;
    public int speed;
    public int armor;
    public int strenght;
    public int id;
    public int teamId;
    private Color color;
    private Color colorBack;

    private SplatterCast m_splatter;

    void Start()
    {
        maxSpeed = speed;
        m_splatter = GetComponent<SplatterCast>();
        Reset();
    }

    public void SetColors(Color color, Color colorBack)
    {
        this.color = color;
        this.colorBack = colorBack;
    }

    public Color getColor() {
        return color;
    }

    public Color getColorBack ()
    {
        return colorBack;
    }

    public float getPorcenHealth ()
    {
        return health / maxHealth;
    }

	public bool isDead() {
		if (health <= 0)
			return true;
		return false;
	}

    public int getMaxSpeed()
    {
        return maxSpeed;
    }

    public void doDamage (int points)
    {
        int finalDamage = (int)(-points * (1.0 - armor / 100.0));
        editHealth(finalDamage);
        GameObject go = Instantiate(Resources.Load("UI/SCT") as GameObject);
        go.transform.SetParent(transform.Find("Canvas").transform);
        go.GetComponent<RectTransform>().localPosition = new Vector3(0.5f, 0.5f, 0f);
        go.GetComponent<Text>().text = Mathf.Abs(finalDamage) + "";
        go.GetComponent<Text>().color = Color.red;
        m_splatter.Play();
    }

    public void doHeal (int points)
    {
        GameObject go = Instantiate(Resources.Load("UI/SCT") as GameObject);
        go.transform.SetParent(transform.Find("Canvas").transform);
        go.GetComponent<RectTransform>().localPosition = Vector3.zero;
        go.GetComponent<Text>().text = Mathf.Abs(points) + "";
        go.GetComponent<Text>().color = Color.green;
        editHealth(points);
    }

    public void editHealth(int points)
    {
        health += points;
        if (health > maxHealth) health = maxHealth;
        else if (health < 0) health = 0;
        Globals.WriteLog("Player: " + id + " Recibe: " + points + " actual: " + health);
    }

	public void Reset () {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Shuriken");
        health = maxHealth;
        foreach (GameObject go in list)
        {
            if (go.GetComponent<SkillBase>().GetOwner() == id)
                Destroy(go);
        }
	}
}
