using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerHandler : MonoBehaviour
{

    [Header("Kinky Bastard")]
    [Tooltip("Add all possible objects to this array - add them linearly to the following to arrays as well")]
    public GameObject[] m_goPossibleObjects;
    [Tooltip("Possible objs displaying a transparent green mesh")]
    public GameObject[] m_goObjPlacementOk;
    [Tooltip("Possible objs displaying a transparent red mesh")]
    public GameObject[] m_goObjPlacementBad;
    public GameObject m_goPlacementDefault;
    public GameObject m_goCurrentlyPlacing;
    public GameObject[] m_goParticleEffects; // 0 = building
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

    private float m_fPlacementHeightOffsetVar;

    // Use this for initialization
    void Start ()
    {
        m_bPlaceShit = false;
        m_fPlacementHeightOffsetVar = 4.6f;
    }

    RaycastHit GenerateRayCast(float _fDistanceOfRay, bool _bUseLayermask)
    {
        RaycastHit _rh;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Ray ray = Camera.main.ScreenPointToRay(m_vec2MouseCoords);
        print(Input.mousePosition);
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
        RaycastHit _rhCheck = GenerateRayCast(m_cMainCam.transform.position.y * 2, false);
        Vector3 pos = _rhCheck.point;
        pos.y = m_fPlacementHeightOffsetVar;
        print("Place turret at: " + pos);
        //pos = new Vector3(Mathf.Round(pos.x / 10) * 10, pos.y, Mathf.Round(pos.z / 10) * 10);
        int cost = m_goPossibleObjects[m_iCurrentlyPlacing].GetComponent<ExpenseHandler>().m_iCostToPlace;
        print("Player Bank: " + gameObject.GetComponent<PlayerHandler>().m_iPlayerCash);
        if (!PlacementUnacceptable(pos))
        {
            if (gameObject.GetComponent<PlayerHandler>().m_iPlayerCash >= cost)
            {
                gameObject.GetComponent<PlayerHandler>().m_iPlayerCash -= cost;
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
        _rhCheck = GenerateRayCast(m_cMainCam.transform.position.y * 2, false);
        print("CHECKIFPLACEMENTOK: RaycastHit point: " + _rhCheck.point);
        Vector3 pos = _rhCheck.point;
        pos.y = m_fPlacementHeightOffsetVar; // CHANGE THIS


        if (!PlacementUnacceptable(pos)) // placement accepted
        {
            m_goPlacementDefault = Instantiate(m_goObjPlacementOk[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
            /* if (m_bBadPlacement)
             {
                 Destroy(m_goPlacementDefault);
                 m_goPlacementDefault = Instantiate(m_goObjPlacementOk[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
                 m_bBadPlacement = false;
             }
             else
             {
                 m_goPlacementDefault.transform.position = pos;
             }*/
            // accepted
        }
        else
        {
            m_goPlacementDefault = Instantiate(m_goObjPlacementBad[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
            /*if (!m_bBadPlacement)
            {
                Destroy(m_goPlacementDefault);
                m_goPlacementDefault = Instantiate(m_goObjPlacementBad[m_iCurrentlyPlacing], pos, Quaternion.identity) as GameObject;
                m_bBadPlacement = true;
            }
            else
            {
                m_goPlacementDefault.transform.position = pos;
            }*/
            // unaccepted
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyUp(KeyCode.P))
        {
            switch(m_bPlaceShit)
            {
                case true:
                    {
                        m_bPlaceShit = false;
                        Destroy(m_goPlacementDefault);
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
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                m_vec2MouseCoords.x = Input.mousePosition.x;
                m_vec2MouseCoords.y = Input.mousePosition.y;
                print(m_vec2MouseCoords.x + ", " + m_vec2MouseCoords.y);
                //  RaycastHit _rhCheck = GenerateRayCast(Camera.main.transform.position.y * 2, true);
                //CheckIfPlacementIsOkay();
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
