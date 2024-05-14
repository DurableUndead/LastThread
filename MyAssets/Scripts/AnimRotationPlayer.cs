using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotationPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("delayRotation", 1f);
    }
    void delayRotation()
    {
        StartCoroutine(PlayerRotationZ());
    }
    IEnumerator PlayerRotationZ()
    {
        while (transform.rotation.eulerAngles.z < 180)
        {
            if (transform.rotation.eulerAngles.z < 120)
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + 50f * Time.deltaTime);
            else if (transform.rotation.eulerAngles.z < 150)
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + 40f * Time.deltaTime);
            else if (transform.rotation.eulerAngles.z < 180)
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + 30f * Time.deltaTime);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }
}
