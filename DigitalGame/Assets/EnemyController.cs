using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float EnemyHealth;
    public float movementspeed;
	// Use this for initialization
	void Start () {
		
	}
	
    void EnemyShot(float damage)
    {
        EnemyHealth -= damage;
    }


	// Update is called once per frame
	void Update () {
        //movementspeed += Time.deltaTime / 10;
        if(EnemyHealth <= 0)
        {
            Destroy(gameObject);
        }


        transform.Translate(Vector3.right * movementspeed * Time.deltaTime);
	}
}
