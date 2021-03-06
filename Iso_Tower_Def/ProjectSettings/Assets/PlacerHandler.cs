﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerHandler : MonoBehaviour
{

    [Header("Kinky Bastard")]
    [Tooltip("Add all possible objects to this array - add them linearly to the following to arrays as well")]
    public List<GameObject> m_goPossibleObjects;
    [Tooltip("Possible objs displaying a transparent green mesh")]
    public List<GameObject> m_goObjPlacementOk;
    [Tooltip("Possible objs displaying a transparent red mesh")]
    public List<GameObject> m_goObjPlacementBad;
    public GameObject m_goPlacementDefault;
    public GameObject m_goCurrentlyPlacing;
    public List<GameObject> m_goParticleEffects; // 0 = building
    public GameObject m_goSuccessfulBuild;
    public List<GameObject> m_goObjsPlaced;
    public int m_iObjsPlaced;
    public int m_iMaxObjsPlaceable;
    public int m_iCurrentlyPlacing;
    private Vector2 m_vec2MouseCoords;
    private bool m_bBadPlacement;
    private bool m_bRefreshBuild;
    public Camera m_cMainCam;
    private bool m_bPlaceShit;
    private GameObject m_goTowerUpgradeUi;
    private float m_fPlacementHeightOffsetVar;
    private float m_fPlacementTimerFix;

    // Use this for initialization
    void Start ()
    {
        m_bPlaceShit = false;
        m_fPlacementHeightOffsetVar = 8.9f;
        m_goTowerUpgradeUi = GameObject.FindGameObjectWithTag("UpgradeUI");
    }

    RaycastHit GenerateRayCast(float _fDistanceOfRay, bool _bUseLayermask)
    {
        RaycastHit _rh;
        Ray ray;
        ray = m_cMainCam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * _fDistanceOfRay, new Color(1f, 0.922f, 0.016f, 1f));
        int _iLayerMask;
        if (_bUseLayermask)
        {
            _iLayerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray.origin, ray.direction * _fDistanceOfRay, out _rh, 250.0f, _iLayerMask))
            {
                return _rh;
            }
            else
            {
                return _rh;
            }
        }
        else
        {
            if (Physics.Raycast(ray.origin, ray.direction * _fDistanceOfRay, out _rh, 250.0f))
            {
                return _rh;
            }
            else
            {
                return _rh;
            }
        }
    }

    private void PlaceAnObject()
    {
        //RaycastHit _rhCheck = GenerateRayCast(Camera.main.transform.position.y * 2, true);
        RaycastHit _rhCheck = GenerateRayCast(m_cMainCam.transform.position.y * 2, true);
        Vector3 pos = _rhCheck.point;
        pos.y = m_fPlacementHeightOffsetVar;
        print("Place turret at: " + pos);
        //pos = new Vector3(Mathf.Round(pos.x / 10) * 10, pos.y, Mathf.Round(pos.z / 10) * 10);
        int cost = m_goPossibleObjects[m_iCurrentlyPlacing].GetComponent<ExpenseHandler>().m_iCostToPlace;
        print("Player Bank: " + PlayerHandler.m_iPlayerCash);
        if (!PlacementUnacceptable(pos) && _rhCheck.transform.tag == "Ground")
        {
            if (PlayerHandler.m_iPlayerCash >= cost)
            {
                PlayerHandler.m_iPlayerCash -= cost;
                // m_goSuccessfulBuild = Instantiate(m_goParticleEffects[0], pos, m_goParticleEffects[0].transform.rotation) as GameObject;
                m_goObjsPlaced.Add(Instantiate(m_goPossibleObjects[m_iCurrentlyPlacing], pos, Quaternion.identity));
                m_goObjsPlaced[m_goObjsPlaced.Count - 1].transform.rotation = m_goPlacementDefault.transform.rotation;
                // m_goObjsPlaced[m_goObjsPlaced.Count - 1].transform.SetParent(GameObject.FindGameObjectWithTag("PlacementObjs").transform);
                Destroy(m_goPlacementDefault);
            }
        }
    }

    private bool PlacementUnacceptable(Vector3 _vec3DesiredPos)
    {
        bool _bObjExists = false;
        if (m_goObjsPlaced.Count != 0)
        {
            for (int i = 0; i < m_goObjsPlaced.Count; i++)
            {
                if (_vec3DesiredPos.x > m_goObjsPlaced[i].transform.position.x - 1.5f && _vec3DesiredPos.x < m_goObjsPlaced[i].transform.position.x + 1.5f)
                {
                    if (_vec3DesiredPos.z > m_goObjsPlaced[i].transform.position.z - 1.5f && _vec3DesiredPos.z < m_goObjsPlaced[i].transform.position.z + 1.5f)
                    {
                        _bObjExists = true;
                    }
                }
            }
        }
        else
        {
            return false;
        }

        return _bObjExists;
    }

    private void CheckIfPlacementIsOkay()
    {
        RaycastHit _rhCheck;
        _rhCheck = GenerateRayCast(m_cMainCam.transform.position.y * 2, true);
        Vector3 pos = _rhCheck.point;
        pos.y = m_fPlacementHeightOffsetVar; // CHANGE THIS

        //  if (!PlacementUnacceptable(pos)) // <--- this placement checking is by position/regional obj positoning only - not ground checking
        if (_rhCheck.transform.tag == "Ground")
        {
            if (!PlacementUnacceptable(pos))
            {
                m_goPlacementDefault = Instantiate(m_goObjPlacementOk[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
            }
        }
        else
        {
            print("UNACCEPTED");
           // m_goPlacementDefault = Instantiate(m_goObjPlacementBad[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_goTowerUpgradeUi.SetActive(false);
            m_fPlacementTimerFix = Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
                switch (m_bPlaceShit)
                {
                    case true:
                        {
                            m_bPlaceShit = false;
                            Destroy(m_goPlacementDefault);
                            m_goTowerUpgradeUi.SetActive(true);
                            break;
                        }
                    case false:
                        {
                            m_bPlaceShit = true;
                            m_vec2MouseCoords.x = Input.mousePosition.x;
                            m_vec2MouseCoords.y = Input.mousePosition.y;
                            CheckIfPlacementIsOkay();
                            break;
                        }
                }
        }

        if (m_bPlaceShit)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                m_iCurrentlyPlacing++;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                m_iCurrentlyPlacing--;
            }

            if(m_iCurrentlyPlacing >= m_goPossibleObjects.Count)
            {
                m_iCurrentlyPlacing -= m_goPossibleObjects.Count;
            }
            if(m_iCurrentlyPlacing < 0)
            {
                m_iCurrentlyPlacing = m_goPossibleObjects.Count - Mathf.Abs(m_iCurrentlyPlacing);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                m_vec2MouseCoords.x = Input.mousePosition.x;
                m_vec2MouseCoords.y = Input.mousePosition.y;
                PlaceAnObject();
            }

            if (m_vec2MouseCoords.x != Input.mousePosition.x || m_vec2MouseCoords.y != Input.mousePosition.y)
            {
                Destroy(m_goPlacementDefault);
                CheckIfPlacementIsOkay();
            }

        }

      
		
	}
}