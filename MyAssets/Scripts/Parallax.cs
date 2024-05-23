using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public float amountOfParallax;
        public float startPosition;
        public float lengthOfSprite;
        public Transform transform;
    }
    [SerializeField] List<ParallaxLayer> parallaxLayers;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.startPosition = layer.transform.position.x;
            layer.lengthOfSprite = layer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    private void FixedUpdate()
    {
        Vector3 Position = mainCamera.transform.position;

        foreach (ParallaxLayer layer in parallaxLayers)
        {
            float Distance = Position.x * layer.amountOfParallax;
            float Temp = Position.x * (1 - layer.amountOfParallax);

            layer.transform.position = new Vector3(layer.startPosition + Distance, layer.transform.position.y, transform.position.z);

            if (Temp > layer.startPosition + layer.lengthOfSprite)
            {
                layer.startPosition += layer.lengthOfSprite;
            }
            else if (Temp < layer.startPosition - layer.lengthOfSprite)
            {
                layer.startPosition -= layer.lengthOfSprite;
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Parallax : MonoBehaviour
// {
//     [System.Serializable]
//     public class ParallaxLayer
//     {
//         public float amountOfParallax;
//         public float startPosition;
//         public float lengthOfSprite;
//         public Transform transform;
//     }
//     [SerializeField] List<ParallaxLayer> parallaxLayers;
//     private Camera mainCamera;
    
//     private void Start()
//     {
//         mainCamera = Camera.main;
//         foreach (ParallaxLayer layer in parallaxLayers)
//         {
//             layer.startPosition = layer.transform.position.x;
//             layer.lengthOfSprite = layer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
//         }
//     }

//     private void FixedUpdate()
//     {
//         Vector3 Position = mainCamera.transform.position;

//         foreach (ParallaxLayer layer in parallaxLayers)
//         {
//             float Temp = Position.x * (1 - layer.amountOfParallax);
//             float Distance = Position.x * layer.amountOfParallax;

//             Vector3 NewPosition = new Vector3(layer.startPosition + Distance, transform.position.y, transform.position.z);
//             layer.transform.position = NewPosition;
//             // transform.position = NewPosition;

//             if (Temp > layer.startPosition + (layer.lengthOfSprite / 2))
//             {
//                 layer.startPosition += layer.lengthOfSprite;
//             }
//             else if (Temp < layer.startPosition - (layer.lengthOfSprite / 2))
//             {
//                 layer.startPosition -= layer.lengthOfSprite;
//             }
//         }
//     }
// }



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Parallax : MonoBehaviour
// {
//     private float startPosition, lengthOfSprite;
//     private Camera mainCamera;
//     [SerializeField] float amountOfParallax;

//     private void Start()
//     {
//         mainCamera = Camera.main;
//         startPosition = transform.position.x;
//         lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
//     }

//     private void FixedUpdate()
//     {
//         Vector3 Position = mainCamera.transform.position;
//         float Temp = Position.x * (1 - amountOfParallax);
//         float Distance = Position.x * amountOfParallax;

//         Vector3 NewPosition = new Vector3(startPosition + Distance, transform.position.y, transform.position.z);

//         transform.position = NewPosition;

//         if (Temp > startPosition + (lengthOfSprite / 2))
//         {
//             startPosition += lengthOfSprite;
//         }
//         else if (Temp < startPosition - (lengthOfSprite / 2))
//         {
//             startPosition -= lengthOfSprite;
//         }
//     }
// }
