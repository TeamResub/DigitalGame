using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    public void rotateCam(string direction)
    {
        if(direction == "left")
        {
            StartCoroutine(Rotation(transform, new Vector3(0,90,0), 0.3f));
        }
        else if (direction == "right")
        {
            StartCoroutine(Rotation(transform, new Vector3(0, -90, 0), 0.3f));
        }
    }

    public IEnumerator Rotation(Transform thisTransform, Vector3 degrees, float time)
    {
        Quaternion startRotation = thisTransform.rotation;
        Quaternion endRotation = thisTransform.rotation * Quaternion.Euler(degrees);
        float rate = 1.0f / time;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            thisTransform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }
    

}
