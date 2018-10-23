using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {
    [Header("Time (in seconds) until object destroys itself")]
    public float m_iTime;
	// Use this for initialization
	void Start () {
        Invoke("Death", m_iTime);
	}
	
	void Death()
    {
        Destroy(gameObject);
    }
}
