using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hqScript : MonoBehaviour {
    public static int hqHealth;
	// Use this for initialization
	void Start () {
        hqHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if(hqHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
	}
}
