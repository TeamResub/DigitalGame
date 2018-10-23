using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public float m_iMovementSpeed;
    public float m_iDamage;
    public enum TurretType {RapidFire, AOE, Pierce};
    public TurretType type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * m_iMovementSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.SendMessage("EnemyShot", m_iDamage);

            //spawn hit particle maybe
            //other stuff for when hit enemy
            if(type != TurretType.Pierce)
            {
                Destroy(gameObject);
            }
        }
    }


}
