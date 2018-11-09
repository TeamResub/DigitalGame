using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiUpdater : MonoBehaviour {
    public bool isRound;
    public bool isHealth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isHealth)
        {
            if (hqScript.hqHealth < 25)
            {
                gameObject.GetComponent<Text>().color = Color.red;
            }
            else if (hqScript.hqHealth < 60)
            {
                gameObject.GetComponent<Text>().color = Color.yellow;
            }
            else
            {
                gameObject.GetComponent<Text>().color = Color.green;
            }
            gameObject.GetComponent<Text>().text = hqScript.hqHealth.ToString();
        }
        else
        {
            if (isRound)
            {
                gameObject.GetComponent<Text>().text = EnemySpawner.g_iRounds.ToString();
            }
            else
            {
                gameObject.GetComponent<Text>().text = PlayerHandler.m_iPlayerCash.ToString();
            }
        }
		
	}
}
