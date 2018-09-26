using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {
    public List<GameObject> targets;

	// Use this for initialization
	void Start () {
		
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
            }
        }

	}
    void OnTriggerEnter2D(Collider2D other)
    {
        print("triggered");
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
