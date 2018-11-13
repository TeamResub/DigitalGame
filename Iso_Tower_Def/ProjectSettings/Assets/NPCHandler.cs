using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCHandler : MonoBehaviour
{
    public NavMeshAgent agent;
    public float m_iHealth;
    private GameObject end;
	// Use this for initialization
	void Start ()
    {
        end = GameObject.FindGameObjectWithTag("CPF");
        agent.SetDestination(end.transform.position);
        m_iHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemySpawner>().npcHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_iHealth <= 0)
        {
            PlayerHandler.m_iPlayerCash += 2;
            Destroy(gameObject);
        }
      
    }
    
    public void EnemyShot(int damage)
    {
        m_iHealth -= damage;
    }

    void OnTriggerEnter(Collider other)
    { 
        if(other.tag == "HQ")
        {
            hqScript.hqHealth -= 1;
            Destroy(gameObject);
        }
    }


}
