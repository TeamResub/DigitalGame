using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
    public float movementspeed;
    public float damage;
    public bool isPierce;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * movementspeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Enemy")
        {

            other.transform.SendMessage("EnemyShot", damage);
            if(!isPierce)
            {
                Destroy(gameObject);
            }
            else
            {
                damage = damage - damage / 5;
            }

        }
    }
}
