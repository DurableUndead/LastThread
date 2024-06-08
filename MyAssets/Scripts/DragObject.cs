using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragObject : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    public int intPuzzleSequence;
    public bool isDragging = false;
    public bool isPuzzleInPlaceRightOrWrong = false;
    // public bool canDrag = true;

    private void OnMouseDown()
    {
        // if (!canDrag)
        //     return;

        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        offset = gameObject.transform.position - GetMouseWorldPos();

        isDragging = true;

        //Shuffel this gameobject
        // transform.position = new Vector2(Random.Range(-5f, 5f),  Random.Range(-5f, 5f));
    }

    private Vector3 GetMouseWorldPos()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object
        mousePoint.z = zCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        // isDragging = true;
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Debug.Log("Puzzle No." + intPuzzleSequence + "> Trigger entered" + other.gameObject);
    //     if (other.gameObject.tag == "Puzzle")
    //     {
    //         Debug.Log("Puzzle No." + intPuzzleSequence + "> Puzzle piece entered the trigger");
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     // Debug.Log("Puzzle No." + intPuzzleSequence + "> Trigger exited" + other.gameObject);
    //     if (other.gameObject.tag == "Puzzle")
    //     {
    //         Debug.Log("Puzzle No." + intPuzzleSequence + "> Puzzle piece exited the trigger");
    //     }
    // }
}
