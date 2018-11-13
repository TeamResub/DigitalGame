using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public float m_iMovementSpeed;
    public float m_iDamage;
    public GameObject SlowObjectPREFAB;
    public enum TurretType {RapidFire, Slow, Pierce};
    public TurretType type;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * m_iMovementSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (type == TurretType.Slow)
            {
                Instantiate(SlowObjectPREFAB, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
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
