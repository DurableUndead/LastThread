using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPuzzlePerPcs : MonoBehaviour
{
    public PuzzleManager scriptPuzzleManager;
    public int intPuzzlePlace;
    public int intGetPuzzlePiece;
    public bool canDetectPuzzlePiece = true;
    public bool puzzleInPlace = false;
    public GameObject thisPuzzlePiece;
    public DragObject scriptDragObject;
    public int defaultOrderInLayer = 2;
    public int noDefaultOrderInLayer = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Puzzle No." + intPuzzlePlace + "> Trigger entered" + other.gameObject);
        if (!canDetectPuzzlePiece)
            return;

        if (other.gameObject.tag == "Puzzle")
        {
            if (thisPuzzlePiece == null)
            {
                thisPuzzlePiece = other.gameObject;
                scriptDragObject = thisPuzzlePiece.GetComponent<DragObject>();
                canDetectPuzzlePiece = false;
                
                //change order in layer
                thisPuzzlePiece.GetComponent<SpriteRenderer>().sortingOrder = noDefaultOrderInLayer;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (puzzleInPlace)
            return;

        if (thisPuzzlePiece != null)
        {
            if(!scriptDragObject.isDragging)
            {
                thisPuzzlePiece.transform.position = this.transform.position;
                puzzleInPlace = true;
                intGetPuzzlePiece = scriptDragObject.intPuzzleSequence;
                scriptDragObject.isPuzzleInPlaceRightOrWrong = true;
                thisPuzzlePiece.GetComponent<SpriteRenderer>().sortingOrder = noDefaultOrderInLayer;
                if (intPuzzlePlace == intGetPuzzlePiece)
                    scriptPuzzleManager.UpdateCondition(true, intPuzzlePlace);
                else
                    scriptPuzzleManager.UpdateCondition(false, intPuzzlePlace);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (thisPuzzlePiece != null)
        {
            if (other.gameObject == thisPuzzlePiece)
            {
                //change order in layer
                thisPuzzlePiece.GetComponent<SpriteRenderer>().sortingOrder = defaultOrderInLayer;
                scriptDragObject.isPuzzleInPlaceRightOrWrong = false;
                scriptPuzzleManager.UpdateCondition(false, intPuzzlePlace);

                thisPuzzlePiece = null;
                scriptDragObject = null;

                puzzleInPlace = false;
                canDetectPuzzlePiece = true;
            }
        }
    }
}
