using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour {
    public bool isPlacing;
    public GameObject tower;
    
    public void Place(GameObject mtower)
    {
        if(GameObject.FindGameObjectWithTag("Placer") == null)
        {
            Instantiate(mtower);
        }
        
    }
	// Use this for initialization
	void Start () {
        isPlacing = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if(isPlacing)
        //{
        //    if(tower != null)
        //    {
        //        tower.transform.position = Input.mousePosition;
        //    }
        //}
	}
}
