﻿using System.Collections;
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
        speed = 1.5f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch(m_eCurrentState)
        {
            case m_eNPCState.START:
                {
                    //get to cp1
                    float step = speed * Time.deltaTime;
                    print("Distance: " + Vector3.Distance(m_goNPCMainTask[1].transform.position, gameObject.transform.position));
                    if (Vector3.Distance(m_goNPCMainTask[1].transform.position, gameObject.transform.position) > 0.0f)
                    {

                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[1].transform.position, step);
                    }
                    else
                    {
                        m_eCurrentState = m_eNPCState.CP1;
                    }
                    break;
                }
            case m_eNPCState.CP1:
                {
                    if (m_iCheckPointCount >= 1) // reach cp2
                    {
                        float step = speed * Time.deltaTime;
                        if (Vector3.Distance(m_goNPCMainTask[2].transform.position,gameObject.transform.position) > 0.0f)
                        {

                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[2].transform.position, step);
                        }
                        else
                        {
                            m_eCurrentState = m_eNPCState.CP2;
                        }
                    }
                    //get to cp1
                    break;
                }
            case m_eNPCState.CP2:
                {
                    if (m_iCheckPointCount >= 2) // reach cp3
                    {
                        float step = speed * Time.deltaTime;
                        if (Vector3.Distance(m_goNPCMainTask[3].transform.position, gameObject.transform.position) > 0.0f)
                        {

                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[3].transform.position, step);
                        }
                        else
                        {
                            if (m_iCheckPointCount == 2)
                            {
                                m_eCurrentState = m_eNPCState.FINISH;
                            }
                            else
                            {
                                m_eCurrentState = m_eNPCState.CP3;
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
                        float step = speed * Time.deltaTime;
                        if (Vector3.Distance(m_goNPCMainTask[4].transform.position, gameObject.transform.position) > 0.0f)
                        {

                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[4].transform.position, step);
                        }
                        else
                        {
                            if (m_iCheckPointCount == 3)
                            {
                                m_eCurrentState = m_eNPCState.FINISH;
                            }
                            else
                            {
                                m_eCurrentState = m_eNPCState.CP4;
                            }
                        }
                    }
                    //get to cp1
                    break;
                }
            case m_eNPCState.FINISH:
                {
                    float step = speed * Time.deltaTime;
                    if (Vector3.Distance(m_goNPCMainTask[m_iCheckPointCount + 1].transform.position, gameObject.transform.position) > 0.0f)
                    {

                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_goNPCMainTask[m_iCheckPointCount + 1].transform.position, step);
                    }
                    else
                    {
                        m_eCurrentState = m_eNPCState.START;
                        gameObject.transform.position = m_goNPCMainTask[0].transform.position;
                    }
                    // head to finish.. restart and teleport to the start..
                    break;
                }
        }
	}
}