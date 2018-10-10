using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {
    public GameObject enemy;
    public List<Transform> nodes;
    public float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
        InvokeRepeating("Spawn", 0.1f, 1f);
	}
	void Update()
    {
        timer += Time.deltaTime;
    }

    void Spawn()
    {
        GameObject temp = Instantiate(enemy, transform.position, enemy.transform.rotation);
        temp.GetComponent<EnemyController>().EnemyHealth += timer / 2;
        temp.GetComponent<EnemyController>().movementspeed += timer / 20;
    }
}
