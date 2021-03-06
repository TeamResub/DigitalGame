﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static int g_iRounds;
    public GameObject npc;
    public float npcHealth;
    private List<GameObject> m_goEnemiesSpawned;
    public List<GameObject> m_goEnemiesToBeSpawned;
    [Header("The spawn amount = no. (m_goEnemiesToBeSpawned) * m_iSpawnAmt")]
    public int m_iSpawnAmount;
    public Text m_tRoundTimer;
    public float m_fSpawnTimer;
	// Use this for initialization
	void Start ()
    {
        g_iRounds = 0;
        m_goEnemiesSpawned = new List<GameObject>();
        npcHealth = 100;
        m_fSpawnTimer = 15.0f;
        //StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnEnemy()
    {
        GameObject cps = GameObject.FindGameObjectWithTag("CPS");
        int num = 0;
        for (int i = 0; i < m_goEnemiesToBeSpawned.Count; i++)
        {
            for (int j = 0; j < m_iSpawnAmount; j++)
            {
                Vector3 temp = new Vector3(cps.transform.position.x + (j * 20), 0, cps.transform.position.z);
                GameObject _temp = Instantiate(m_goEnemiesToBeSpawned[i], temp, Quaternion.identity) as GameObject;
                m_goEnemiesSpawned.Add(_temp);
                print("spawned at " + temp);
            }
            //cps.transform.position = new Vector3(cps.transform.position.x, cps.transform.position.y, -10 + cps.transform.position.z);
            //num += 1;
            yield return new WaitForSeconds(3);
        }
        print("[SPAWNED]: " + m_iSpawnAmount * m_goEnemiesToBeSpawned.Count + " enemies");
        // cps.transform.position = new Vector3(cps.transform.position.x, cps.transform.position.y, cps.transform.position.z + 10*num);
    }


    public void NextRound()
    {
        if (g_iRounds != 0)
        {
            PlayerHandler.m_iPlayerCash += 15 * g_iRounds;
        }
        g_iRounds++;
        m_goEnemiesToBeSpawned.Add(npc);
        npcHealth += g_iRounds * 10;
        m_fSpawnTimer = 15.0f * g_iRounds;
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
            // start counter
            m_tRoundTimer.text = "Spawn Timer: " + m_fSpawnTimer;
            if (m_fSpawnTimer < 0)
            {
                print("SPAWN THE NPCS");
                NextRound();
                m_tRoundTimer.text = "Spawn Time: SPAWNED";
            }
            else
            {
                m_fSpawnTimer -= Time.deltaTime;
            }
        }

        if (m_goEnemiesSpawned.Count <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            NextRound();
            m_tRoundTimer.text = "Spawn Time: SPAWNED";
        }
        /***
         * 
         * could do this so many ways.. check for when all of the unit's have 0 health, or reset by round because either way when all npcs have 0 health the round is ultimately
         * restarting...
         * 
         ***/
    }
}
