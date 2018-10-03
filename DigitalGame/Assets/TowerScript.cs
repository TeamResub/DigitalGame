using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {
    public List<GameObject> targets;
    public float AttackSpeed;
    private bool canAttack;
    public GameObject Projectile;
	// Use this for initialization
	void Start () {
        canAttack = true;
	}
	
    void AttackCooldown()
    {
        canAttack = true;
    }


	// Update is called once per frame
	void Update () {
        

        if(targets.Count > 0)
        {
            if(targets[0] == null)
            {
                targets.RemoveAt(0);
            }
            else
            {
                transform.up = targets[0].transform.position - transform.position;
                if(canAttack)
                {
                    Instantiate(Projectile, transform.position, transform.rotation);
                    canAttack = false;
                    Invoke("AttackCooldown", AttackSpeed);

                }
            }
        }

	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            targets.Insert(targets.Count, other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Remove(other.gameObject);
        }
    }
}
