using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {
    [Header("Prefab of Enemy")]
    public GameObject enemy;
    [Header("List Of Points in the Enemies Pathfinding")]
    public List<Transform> nodes;
    private float timer;
    [Header("Rate At Which Enemies Spawn")]
    public float spawnRate;
	// Use this for initialization
	void Start () {
        timer = 0;
        InvokeRepeating("Spawn", 0.1f, spawnRate);
	}
	void Update()
    {
        timer += Time.deltaTime;
    }

    void Spawn()
    {
        GameObject temp = Instantiate(enemy, transform.position, enemy.transform.rotation);
        temp.GetComponent<EnemyController>().EnemyHealth += timer / 2;
        //temp.GetComponent<EnemyController>().movementspeed += timer / 20;
    }
}
