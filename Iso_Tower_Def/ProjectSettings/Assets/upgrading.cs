using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrading : MonoBehaviour {
    public GameObject target;
    public SellectedObjectUI ui;
    public Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            int layermask = LayerMask.GetMask("Turret");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f, 0.922f, 0.016f, 1f));
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, layermask))
            {
                
                print("hit " + hit.transform.name);
                target = hit.transform.gameObject;
            }
            else
            {
                print("no hit");
                //target = null;
            }
        }

        

        if(target != null)
        {
            ui.gameObject.SetActive(true);
            ui.name.text = target.GetComponent<uiValues>().sname;
            ui.level.text = target.transform.GetChild(0).GetComponent<TurretController>().level.ToString();
            ui.speed.text = target.transform.GetChild(0).GetComponent<TurretController>().fireRate.ToString();

        }
        else
        {
            //ui.gameObject.SetActive(false);
        }
	}




    public void upgrade()
    {
        if(PlayerHandler.m_iPlayerCash >= 50)
        {
            target.transform.GetChild(0).GetComponent<TurretController>().Upgrade();
            PlayerHandler.m_iPlayerCash -= 50;
        }
        
    }
}
