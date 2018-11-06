using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCHandler : MonoBehaviour
{
    public NavMeshAgent agent;
    public int m_iHealth;
    private GameObject end;
	// Use this for initialization
	void Start ()
    {
        end = GameObject.FindGameObjectWithTag("CPF");
        agent.SetDestination(end.transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_iHealth <= 0)
        { 
            Destroy(gameObject);
        }
      
    }
    
    public void EnemyShot(int damage)
    {
        m_iHealth -= damage;
    }

}
