using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * 
 * This class will dynamically load multiple start, mid checkpoints and finish points.
 * which will handle the process of the actual NPC mechanism
 * 
 ***/

public class NPCHandler : MonoBehaviour
{
    public int m_iHealth;
    private List<GameObject> m_goNPCMainTask;
    public enum m_eNPCState
    {
        START, CP1,CP2,CP3,CP4,CP5, FINISH
    };
    public m_eNPCState m_eCurrentState;
    private int m_iCheckPointCount;
    private float speed;
    private GameObject m_goTarget; // need a target because on mass spawning using a formula to determine current target bugs the fuck out
	// Use this for initialization
	void Start ()
    {
        m_goNPCMainTask = new List<GameObject>();
        m_eCurrentState = m_eNPCState.START;
        if (GameObject.FindGameObjectWithTag("CPS"))
        {
            //m_goNPCMainTask.Insert(0,GameObject.FindGameObjectWithTag("CPS"));
            m_goNPCMainTask.Add(GameObject.FindGameObjectWithTag("CPS")); // start
        }
        else
        {
            print("CANNOT LOAD CHECKPOINT START...");
        }
        gameObject.transform.position = GameObject.FindGameObjectWithTag("CPS").transform.position;
        for (int i = 0; i < 5; i++ ) // could have a defined public int for max checkpoints or some shit idek. currently like 5
        {
            if (GameObject.FindGameObjectWithTag("CP" + (i+1)))
            {
                m_goNPCMainTask.Add(GameObject.FindGameObjectWithTag("CP" + (i + 1)));
                m_iCheckPointCount++;
            }
            else
            {
                break; // break out because we have obviously exceeded the checkpoint range within the current level
            }
        }
        m_goNPCMainTask.Add(GameObject.FindGameObjectWithTag("CPF")); // finish


        /***
         * 
         * make this speed val change by round or some bs..
         * 
         ***/
        speed = Random.Range(0.3f, 2.0f);
        //m_iHealth = (int)Random.Range(15.0f, 30.0f);
        m_iHealth = 100;
        // assign target
        m_goTarget = m_goNPCMainTask[1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_iHealth > 0)
        {
            float step = speed * Time.deltaTime;
            switch (m_eCurrentState)
            {
                case m_eNPCState.START:
                    {
                        //get to cp1
                        if (Vector3.Distance(m_goNPCMainTask[1].transform.position, gameObject.transform.position) > 0.0f)
                        {

                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[1].transform.position, step);
                        }
                        else
                        {
                            m_goTarget = m_goNPCMainTask[2];
                            m_eCurrentState = m_eNPCState.CP1;
                        }
                        break;
                    }
                case m_eNPCState.CP1:
                    {
                        if (m_iCheckPointCount >= 1) // reach cp2
                        {
                            if (Vector3.Distance(m_goNPCMainTask[2].transform.position, gameObject.transform.position) > 0.0f)
                            {

                                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[2].transform.position, step);
                            }
                            else
                            {
                                if (m_iCheckPointCount == 1)
                                {
                                    m_eCurrentState = m_eNPCState.FINISH;
                                    m_goTarget = m_goNPCMainTask[m_iCheckPointCount + 1];
                                }
                                else
                                {
                                    m_eCurrentState = m_eNPCState.CP2;
                                    m_goTarget = m_goNPCMainTask[3];
                                }
                            }
                        }
                        //get to cp1
                        break;
                    }
                case m_eNPCState.CP2:
                    {
                        if (m_iCheckPointCount >= 2) // reach cp3
                        {
                            if (Vector3.Distance(m_goNPCMainTask[3].transform.position, gameObject.transform.position) > 0.0f)
                            {

                                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[3].transform.position, step);
                            }
                            else
                            {
                                if (m_iCheckPointCount == 2)
                                {
                                    m_eCurrentState = m_eNPCState.FINISH;
                                    m_goTarget = m_goNPCMainTask[m_iCheckPointCount + 1];
                                }
                                else
                                {
                                    m_eCurrentState = m_eNPCState.CP3;
                                    m_goTarget = m_goNPCMainTask[4];
                                }
                            }
                        }
                        //get to cp1
                        break;
                    }
                case m_eNPCState.CP3:
                    {
                        if (m_iCheckPointCount >= 3) // reach cp3
                        {
                            if (Vector3.Distance(m_goNPCMainTask[4].transform.position, gameObject.transform.position) > 0.0f)
                            {

                                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[4].transform.position, step);
                            }
                            else
                            {
                                if (m_iCheckPointCount == 3)
                                {
                                    m_eCurrentState = m_eNPCState.FINISH;
                                    m_goTarget = m_goNPCMainTask[m_iCheckPointCount + 1];
                                }
                                else
                                {
                                    m_eCurrentState = m_eNPCState.CP4;
                                    m_goTarget = m_goNPCMainTask[5];
                                }
                            }
                        }
                        //get to cp1
                        break;
                    }
                case m_eNPCState.FINISH:
                    {
                        if (Vector3.Distance(m_goNPCMainTask[m_iCheckPointCount + 1].transform.position, gameObject.transform.position) > 0.0f)
                        {

                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[m_iCheckPointCount + 1].transform.position, step);
                        }
                        else
                        {
                            m_eCurrentState = m_eNPCState.START;
                            gameObject.transform.position = m_goNPCMainTask[0].transform.position;
                            m_goTarget = m_goNPCMainTask[1];
                        }
                        // head to finish.. restart and teleport to the start..
                        break;
                    }
                default:
                    break;
            }
        }
        else
        {
            Destroy(gameObject);
        }
      
        // Make the NPC look at it's target...
        // this.transform.LookAt(m_goNPCMainTask[(int)m_eCurrentState+1].transform.position); // this bugs out when you have a mass amount of npcs spawned...
        this.transform.LookAt(m_goTarget.transform.position);
    }
    
    public void EnemyShot(int damage)
    {
        m_iHealth -= damage;
    }

}
