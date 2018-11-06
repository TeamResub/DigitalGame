using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followmouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-5 *Time.deltaTime, 0 , 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 5 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -5 * Time.deltaTime, 0);
        }


        if(transform.position.x > 0)
        {
            Destroy(gameObject);
        }
    }
}
