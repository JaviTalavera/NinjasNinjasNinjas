using UnityEngine;

public class FireSkill1 : SkillBase {
    
    public int m_distance;
    public float m_speed;
    private bool m_movimiento = true;
    public ParticleSystem m_colission_suelo;
    public ParticleSystem m_pick_item;

    private Vector3 target;
    private Transform m_transform_sombra;
    private Collider m_collider;
	public static int[] municion = {3,3,3,3};
    public bool m_gastaMunicion;


    // Use this for initialization
    void Start () {
        m_transform_sombra = transform.GetChild(1);
        m_collider = GetComponent<Collider>();
    }
    

    // Update is called once per frame
    void Update () {
        if (m_movimiento)
        {
            Vector3 dir = transform.position - target;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * m_speed);
            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                StopMovimiento();
            }
        }
    }

    public void StopMovimiento()
    {
        this.m_movimiento = false;
        this.transform.GetChild(0).GetComponent<SelfRotate>().RotateStop();
        this.m_collider.isTrigger = true;
        this.m_pick_item.startColor = m_handler.GetComponent<PlayerStats>().getColor();
        this.m_pick_item.Play();
        this.m_particle.Stop();
        this.IgnoreCollision(false);
        Vector3 newPos = new Vector3(transform.position.x, 0.01f, transform.position.z);
        transform.position = newPos;
        GameObject.Instantiate(m_colission_suelo, m_transform_sombra.position, m_colission_suelo.transform.rotation);
    }

    public override GameObject Throw(Vector3 position, Vector3 direccion, SkillHandler handler, int owner, int team)
    {
		if (FireSkill1.municion[owner-1] > 0)
        {
            GameObject go = GameObject.Instantiate(this.gameObject, new Vector3(position.x, 0.4f, position.z), Quaternion.identity);
            go.GetComponent<FireSkill1>().target = position + direccion.normalized * m_distance + new Vector3(0, 0.4f, 0);
            go.GetComponent<FireSkill1>().m_direccion = direccion;
            go.GetComponent<FireSkill1>().m_skillId = Random.Range(0, 10000);
            go.GetComponent<FireSkill1>().m_owner = owner;
            go.GetComponent<FireSkill1>().m_team = team;
            go.GetComponent<FireSkill1>().m_handler = handler;
            go.GetComponent<FireSkill1>().IgnoreCollision(true);
            FireSkill1.municion[owner-1]--;
			Globals.WriteLog("Municion (" + owner + "): " + FireSkill1.municion[owner-1]);
            return go;
        }
        return null;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && m_team != collision.gameObject.GetComponent<PlayerStats>().teamId)
        {
            DoDamage(collision.gameObject, collision.contacts[0].point, 0f);
            IgnoreCollision(collision.collider, true);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("World"))
            StopMovimiento();
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            StopMovimiento();
    }
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && m_owner == other.GetComponent<PlayerStats>().id)
        {
            FireSkill1.municion[m_owner - 1]++;
            gameObject.GetComponent<FireSkill1>().m_handler.AddMunicion();
			Globals.WriteLog("Municion (" + m_owner + "): " + FireSkill1.municion[m_owner-1]);
            Destroy(gameObject);
        }
    }
}
