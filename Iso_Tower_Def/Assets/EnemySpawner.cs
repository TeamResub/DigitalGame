using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<GameObject> m_goEnemiesSpawned;
    public List<GameObject> m_goEnemiesToBeSpawned;
    public GameObject m_goNPC1;
    [Header("The spawn amount = no. (m_goEnemiesToBeSpawned) * m_iSpawnAmt")]
    public int m_iSpawnAmount;
	// Use this for initialization
	void Start ()
    {
        m_goEnemiesSpawned = new List<GameObject>();



        for (int i = 0; i < m_goEnemiesToBeSpawned.Count; i++)
        {
            for (int j = 0; j < m_iSpawnAmount; j++)
            {
                //m_goEnemiesSpawned[j] = Instantiate(m_goEnemiesToBeSpawned[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                GameObject _temp = Instantiate(m_goEnemiesToBeSpawned[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                m_goEnemiesSpawned.Add(_temp);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
