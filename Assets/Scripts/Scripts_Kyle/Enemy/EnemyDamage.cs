using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health = 100;
    public EnemyStats EnemyStats;
    public Slingshot Slingshot; 

    private float attackDamage;

    void Start()
    {
        if (Slingshot != null)
        {
            //attackDamage = Slingshot.attackdamage;
        }
        else
        {
            attackDamage = 1.0f; 
        }
    }

    public void Update()
    {
        if (Slingshot == null)
        {
            Slingshot = GetComponent<Slingshot>();
        }
    }

    public void InflictDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeMeleeDamage(float attackDamage)
    {
        InflictDamage((int)attackDamage);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Sphere")
        {
            InflictDamage((int)attackDamage);
        }
    }
}