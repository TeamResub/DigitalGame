using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelf : MonoBehaviour {
    public float timeToDelete;
	// Use this for initialization
	void Start () {
        Invoke("DeleteMe", timeToDelete);
	}

    void DeleteMe()
    {
        Destroy(gameObject);
    }
}
