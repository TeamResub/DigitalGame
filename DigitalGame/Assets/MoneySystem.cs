using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour {
    public static int Money;
    public int startingmoney;
	// Use this for initialization
	void Start () {
        Money = startingmoney;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Money < 0)
        {
            Money = 0;
        }
	}
}
