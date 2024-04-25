using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosition, lengthOfSprite;
    private Camera mainCamera;
    [SerializeField] float amountOfParallax;

    private void Start()
    {
        mainCamera = Camera.main;
        startPosition = transform.position.x;
        lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        Vector3 Position = mainCamera.transform.position;
        float Temp = Position.x * (1 - amountOfParallax);
        float Distance = Position.x * amountOfParallax;

        Vector3 NewPosition = new Vector3(startPosition + Distance, transform.position.y, transform.position.z);

        transform.position = NewPosition;

        if (Temp > startPosition + (lengthOfSprite / 2))
        {
            startPosition += lengthOfSprite;
        }
        else if (Temp < startPosition - (lengthOfSprite / 2))
        {
            startPosition -= lengthOfSprite;
        }
    }
}
