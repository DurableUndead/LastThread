using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [Header("Scripts")]
    public Chapter1Scene scriptChapter1Scene;

    [Space]
    public GameObject[] puzzlePieces;
    public GameObject[] puzzlePlaceholders;
    public Dictionary<int, bool> dictPuzzleInPlace;
    [Header("Button Menu Puzzle")]
    public Button[] buttonMenuPuzzle;

    [Header("Random Position")]
    public float randomMinX = -5f;
    public float randomMaxX = 5f;
    public float randomMinY = -5f;
    public float randomMaxY = 5f;
    public Transform shufflePlaceholdersGO;

    // Start is called before the first frame update
    void Start()
    {
        // scriptChapter1Scene = GetComponent<Chapter1Scene>();
        scriptChapter1Scene = GameObject.Find("Canvas").GetComponent<Chapter1Scene>();

        dictPuzzleInPlace = new Dictionary<int, bool>() 
        { 
            { 0, false }, 
            { 1, false }, 
            { 2, false }, 
            { 3, false }, 
            { 4, false }, 
            { 5, false }, 
            { 6, false }, 
            { 7, false }, 
            { 8, false }, 
            { 9, false },
            { 10, false }, 
            { 11, false }
        };
        ResetPuzzle();
    }

    public void CanDragObject()
    {
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            //box collider 2d
            puzzlePiece.GetComponent<BoxCollider2D>().enabled = true;
        }

        foreach (Button button in buttonMenuPuzzle)
        {
            button.interactable = true;
        }
    }

    public void CannotDragObject()
    {
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            puzzlePiece.GetComponent<BoxCollider2D>().enabled = false;
        }

        foreach (Button button in buttonMenuPuzzle)
        {
            button.interactable = false;
        }
    }

    public void ResetPuzzle()
    {
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            puzzlePiece.transform.position = shufflePlaceholdersGO.position;
            float randomX = shufflePlaceholdersGO.position.x + Random.Range(randomMinX, randomMaxX);
            float randomY = shufflePlaceholdersGO.position.y + Random.Range(randomMinY, randomMaxY);
            Vector2 randomPosition = new Vector2(randomX, randomY);
            puzzlePiece.transform.position = randomPosition;
        }
    }

    public void ResetConditionPuzzle()
    {
        // Menggunakan daftar kunci untuk iterasi aman
        List<int> keys = new List<int>(dictPuzzleInPlace.Keys);
        foreach (int key in keys)
        {
            dictPuzzleInPlace[key] = false;
        }
    }

    public void ShuffleThePuzzle()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool inPlace = puzzlePieces[i].GetComponent<DragObject>().isPuzzleInPlaceRightOrWrong;
            if (inPlace == false)
            {
                ShufflePerPuzzle(i);
            }
        }
    }

    void ShufflePerPuzzle(int puzzleIndex)
    {
        puzzlePieces[puzzleIndex].transform.position = shufflePlaceholdersGO.position;
        float randomX = shufflePlaceholdersGO.position.x + Random.Range(randomMinX, randomMaxX);
        float randomY = shufflePlaceholdersGO.position.y + Random.Range(randomMinY, randomMaxY);
        Vector2 randomPosition = new Vector2(randomX, randomY);
        puzzlePieces[puzzleIndex].transform.position = randomPosition;
    }

    public void UpdateCondition(bool isPuzzleRightInPlace, int puzzleIndex)
    {
        puzzleIndex -= 1;
        dictPuzzleInPlace[puzzleIndex] = isPuzzleRightInPlace;
        CheckPuzzle();
    }

    void CheckPuzzle()
    {
        bool isAllPuzzleInPlace = true;
        foreach (KeyValuePair<int, bool> puzzle in dictPuzzleInPlace)
        {
            if (!puzzle.Value)
            {
                isAllPuzzleInPlace = false;
                break;
            }
        }

        if (isAllPuzzleInPlace)
        {
            scriptChapter1Scene.ClosePuzzleJigsaw(puzzleWin: true);
        }
    }
}
