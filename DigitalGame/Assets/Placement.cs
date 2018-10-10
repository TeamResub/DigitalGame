using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour {
    public bool isPlacing;
    public GameObject tower;
    public List<GameObject> placedObjs;
    public bool m_bPlayerPlacing;
    public void Place(GameObject mtower)
    {
     
        if (GameObject.FindGameObjectWithTag("Placer") == null)
        {
            m_bPlayerPlacing = true;
            Instantiate(mtower);
        }
    }
	// Use this for initialization
	void Start () {
        isPlacing = false;
        m_bPlayerPlacing = false;
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
