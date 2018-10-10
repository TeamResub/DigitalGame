using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float EnemyHealth;
    public int worth;
    public float movementspeed;
    public GameObject Parental;
    public Transform currentTarget;
    private int index;
	// Use this for initialization
	void Start () {
        index = 0;
        Parental = GameObject.FindGameObjectWithTag("Spawner");
        currentTarget = Parental.GetComponent<SpawnerController>().nodes[index];
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
            MoneySystem.Money += worth;
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            index++;
            if(index >= Parental.GetComponent<SpawnerController>().nodes.Count)
            {
                Destroy(gameObject);
            }
            else
            {
                currentTarget = Parental.GetComponent<SpawnerController>().nodes[index];
            }
            
        }



        transform.up = currentTarget.position - transform.position; 
        
        transform.Translate(Vector3.up * movementspeed * Time.deltaTime);
	}
}
