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


    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < m_goEnemiesToBeSpawned.Count; i++)
        {
            for (int j = 0; j < m_iSpawnAmount; j++)
            {
                GameObject _temp = Instantiate(m_goEnemiesToBeSpawned[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                m_goEnemiesSpawned.Add(_temp);
                yield return new WaitForSeconds(0.5f);
            }
        }
        print("[SPAWNED]: " + m_iSpawnAmount * m_goEnemiesToBeSpawned.Count + " enemies");
    }


    public void NextRound()
    {
        g_iRounds++;
        m_iSpawnAmount += 1 + g_iRounds;
        StartCoroutine(SpawnEnemy());
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
