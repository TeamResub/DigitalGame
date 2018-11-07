using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int g_iRounds;

    private List<GameObject> m_goEnemiesSpawned;
    public List<GameObject> m_goEnemiesToBeSpawned;
    [Header("The spawn amount = no. (m_goEnemiesToBeSpawned) * m_iSpawnAmt")]
    public int m_iSpawnAmount;
	// Use this for initialization
	void Start ()
    {
        g_iRounds = 0;
        m_goEnemiesSpawned = new List<GameObject>();
        //StartCoroutine(SpawnEnemy());
    }


    void SpawnEnemy()
    {
        GameObject cps = GameObject.FindGameObjectWithTag("CPS");
        for (int i = 0; i < m_goEnemiesToBeSpawned.Count; i++)
        {
            for (int j = 0; j < m_iSpawnAmount; j++)
            {
                Vector3 temp = new Vector3(cps.transform.position.x + (j * 10), 0, cps.transform.position.z);
                GameObject _temp = Instantiate(m_goEnemiesToBeSpawned[i], temp, Quaternion.identity) as GameObject;
                m_goEnemiesSpawned.Add(_temp);
                print("spawned at " + temp);
            }
            cps.transform.position = new Vector3(cps.transform.position.x, cps.transform.position.y, -10 + cps.transform.position.z);
        }
        print("[SPAWNED]: " + m_iSpawnAmount * m_goEnemiesToBeSpawned.Count + " enemies");
    }


    public void NextRound()
    {
        g_iRounds++;
        m_iSpawnAmount += 1 + g_iRounds;
        SpawnEnemy();
    }

	// Update is called once per frame
	void Update ()
    {

        for(int i = 0; i < m_goEnemiesSpawned.Count; i++)
        {
            if(m_goEnemiesSpawned[i] == null)
            {
                m_goEnemiesSpawned.RemoveAt(i);
            }
        }


        if(m_goEnemiesSpawned.Count <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                NextRound();
            }
        }
		/***
         * 
         * could do this so many ways.. check for when all of the unit's have 0 health, or reset by round because either way when all npcs have 0 health the round is ultimately
         * restarting...
         * 
         ***/
	}
}
