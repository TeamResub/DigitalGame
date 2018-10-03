using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {
    public GameObject enemy;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 0.1f, 1f);
	}
	

    void Spawn()
    {
        Instantiate(enemy, transform.position, enemy.transform.rotation);
    }
}
