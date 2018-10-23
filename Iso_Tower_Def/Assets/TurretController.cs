using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    public enum eAIMode { Idle, Alert, Aggro };
    public eAIMode myAIMode;
    public enum eTurretLevel { First, Second, Third, Fourth };
    public eTurretLevel myTurretLvl;
    public float damping;
    public Transform endofturret;
    //public Animator anim;
    public float fireRate;
    //public float turretAccuracy;
    private float turretCooldown;
    public AudioSource gunShotSound;
    public GameObject projectile;
    public float f_TurretHealth;
    //public GameObject upgradeParticle;
    public List<GameObject> Targets;


    void Start()
    {
        print(f_TurretHealth);
        myAIMode = eAIMode.Idle;
        turretCooldown = 0;
    }



    void Update()
    {
        for (int i = 0; i < Targets.Count; i++)
        {
            if(Targets[i] == null)
            {
                Targets.RemoveAt(i);
            }
        }



        if (Targets.Count <= 0)
        {
            myAIMode = eAIMode.Idle;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(Targets.Count);
        }

        switch (myAIMode)
        {
            case eAIMode.Idle:
                {
                    /*if (transform.eulerAngles.x != 0)
                    {
                        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                    }

                    transform.Rotate(0, 10 * Time.deltaTime, 0);*/
                    break;
                }
            case eAIMode.Aggro:
                {
                    if (Targets[0] != null)
                    {
                        /*Vector3 lookpos = Targets[0].transform.position - transform.position;
                        //lookpos.y = 0;
                        Quaternion rotation = Quaternion.LookRotation(lookpos);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
                        */
                        transform.LookAt(Targets[0].transform.GetChild(1).position);
                        Vector3 eulerangles = transform.eulerAngles;
                        eulerangles.x = 0;
                        eulerangles.z = 0;
                        eulerangles.y += 90;

                        transform.rotation = Quaternion.Euler(eulerangles);

                        if (Time.time > turretCooldown)
                        {
                            Vector3 rayOrigin = endofturret.position;
                            Vector3 rayDirection = Targets[0].transform.GetChild(1).position - endofturret.position;
                            RaycastHit hit;
                            int layermask = LayerMask.GetMask("Enemy");
                            if (Physics.Raycast(endofturret.position, rayDirection, out hit, 100, layermask))
                            {
                                if (Physics.Raycast(endofturret.position, rayDirection, out hit, 100, layermask))
                                {
                                    Debug.DrawRay(endofturret.position, rayDirection, Color.yellow);
                                    Debug.Log(hit.transform.name);
                                    CheckHit(hit, rayDirection);
                                }
                                else
                                {
                                    Debug.DrawRay(endofturret.position, rayDirection, Color.white);
                                    CheckHit(hit, rayDirection);
                                }
                                //anim.Play("Fire-Reload");
                                gunShotSound.Play();
                                turretCooldown = Time.time + fireRate;
                                
                            }
                            else
                            {
                                Debug.DrawRay(endofturret.position, rayDirection, Color.white);
                            }
                        }
                        
                     
                    }
                    break;
                }
        }
        if (f_TurretHealth <= 0)
        {
            TurretDeath();
        }
    }


    void TurretDeath()
    {
        //give player xp
        //spawn any particle effects
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            myAIMode = eAIMode.Aggro;
            Targets.Insert(Targets.Count, other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Targets.Remove(other.gameObject);
        }
    }

    void CheckHit(RaycastHit hit, Vector3 rayDirection)
    {
        //yield return new WaitForSeconds(0.55f);
        GameObject MissileObj = Instantiate(projectile, endofturret.position, Quaternion.Euler(new Vector3(rayDirection.x, rayDirection.y, rayDirection.z)));
        MissileObj.transform.up = rayDirection;
    }

    void TurretShot(float damage)
    {
        f_TurretHealth -= damage;
        print(f_TurretHealth);
    }

    public void UpgradeTo(eTurretLevel lvl)
    {
        if (lvl == eTurretLevel.Second)
        {
            //Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
            //PublicStats.FlameBallDamage = 70;
            //PublicStats.FlameBallRadius += 1;
            ////turretDamage += turretDamage;
            //fireRate -= fireRate *= 0.2f;
            //myTurretLvl = eTurretLevel.Second;
        }
        if (lvl == eTurretLevel.Third)
        {
            //Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
            //PublicStats.FlameBallRadius += 1;
            //PublicStats.FlameBallDamage = 80;
            ////turretDamage += turretDamage;
            //fireRate -= fireRate *= 0.2f;
            //myTurretLvl = eTurretLevel.Third;
        }
        if (lvl == eTurretLevel.Fourth)
        {
            //Instantiate(upgradeParticle, transform.position, upgradeParticle.transform.rotation);
            //PublicStats.FlameBallRadius += 1;
            //PublicStats.FlameBallDamage = 90;
            ////turretDamage += turretDamage;
            //fireRate -= fireRate *= 0.2f;
            //myTurretLvl = eTurretLevel.Fourth;
        }
    }
}
