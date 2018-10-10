using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObj : MonoBehaviour {
    public GameObject TowerFinal;
    public GameObject good;
    public GameObject bad;
    private GameObject placement;
    public AudioHandler m_ah;
    private bool canPlace;
    private bool isPlacing;
	// Use this for initialization
	void Start () {
        canPlace = true;
        placement = GameObject.FindGameObjectWithTag("Player");
        isPlacing = GameObject.FindGameObjectWithTag("Player").GetComponent<Placement>().isPlacing;
        m_ah = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioHandler>();
    }

    private void DestroyPlacementObj()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Placement>().isPlacing = false;
        Destroy(GameObject.FindGameObjectWithTag("Placer"));
        // Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<Placement>().);
        isPlacing = false;
        print("Destroy placement prefab");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DestroyPlacementObj();
        }

        if (canPlace)
        {
            good.SetActive(true);
            bad.SetActive(false);
        }
        else
        {
            good.SetActive(false);
            bad.SetActive(true);
        }


        for (int i = 0; i < placement.GetComponent<Placement>().placedObjs.Count; i++)
        {
            if(Vector3.Distance(transform.position, placement.GetComponent<Placement>().placedObjs[i].transform.position) < 1.0f)
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

            if (canPlace)
            {
                GameObject temp = Instantiate(TowerFinal, transform.position, transform.rotation);
                placement.GetComponent<Placement>().placedObjs.Add(temp);
                MoneySystem.Money -= TowerFinal.GetComponent<TowerScript>().price;
                Destroy(gameObject);

            }
            else
            {
                //DestroyPlacementObj();
                m_ah.PlaySound(AudioHandler.m_soundTypes.DAMAGE);
                print("NOT ENUF $$");
            }

        }

    }
}
