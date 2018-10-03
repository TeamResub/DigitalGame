using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObj : MonoBehaviour {
    public GameObject TowerFinal;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(TowerFinal, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
