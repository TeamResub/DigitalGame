using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownOBJ : MonoBehaviour {
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2);
        foreach (Collider hit in hitColliders)
        {
            if (hit.tag == "Enemy")
            {
                hit.GetComponent<NPCHandler>().speed -= hit.GetComponent<NPCHandler>().speed/3;
            }
        }
    }
}
