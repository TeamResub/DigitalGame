using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObj : MonoBehaviour {
    public GameObject TowerFinal;
    public GameObject good;
    public GameObject bad;
    private bool canPlace;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if(canPlace)
        {
            good.SetActive(true);
            bad.SetActive(false);
        }
        else
        {
            good.SetActive(false);
            bad.SetActive(true);
        }


        for (int i = 0; i < Placement.placedObjs.Count; i++)
        {
            if(Vector3.Distance(transform.position, Placement.placedObjs[i].transform.position) < 0.5f)
            {
                canPlace = false;
                break;
            }
            else
            {
                canPlace = true;
            }
        }



        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(MoneySystem.Money >= TowerFinal.GetComponent<TowerScript>().price)
            {
                GameObject temp = Instantiate(TowerFinal, transform.position, transform.rotation);
                Placement.placedObjs.Add(temp);
                MoneySystem.Money -= TowerFinal.GetComponent<TowerScript>().price;
                Destroy(gameObject);
            }
        }

    }
}
