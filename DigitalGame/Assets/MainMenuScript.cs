using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}

    public void ClickCallBack(int _button)
    {
        switch(_button)
        {
            case 0: // play
                {
                    SceneManager.LoadScene("Scene");
                    break;
                }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
