using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float speedScroll = -5f;
    public float targetX = -19;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speedScroll * Time.deltaTime, 0);
        if (transform.position.x < targetX)
        {
            transform.position = new Vector3(targetX * -1, transform.position.y);
        }
    }
}
