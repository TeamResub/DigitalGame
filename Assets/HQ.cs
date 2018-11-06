using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQ : MonoBehaviour {
    public int health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (health <= 0)
        {
            print("you lose");
        }
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);

            health -= 1;
        }
    }
}
