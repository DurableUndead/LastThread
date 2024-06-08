using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CindyState
{
    SideFacingRight,
    SideFacingLeft,
    IdleFacingRight,
    IdleFacingLeft
}

public enum CindyMovementType
{
    ManualMovement,
    AutoMovementRight,
    AutoMovementLeft,
}
public class CindyMovement : MonoBehaviour
{
    private List<System.Action> functionMovementCindy = new List<System.Action>();
    [Header("Cindy GameObjects")]
    public Rigidbody2D rb2D;
    public GameObject cindyFacingRightGO;
    public GameObject cindyFacingLeftGO;
    public GameObject cindyIdleFacingRightGO;
    public GameObject cindyIdleFacingLeftGO;
    public Transform cindyLookAtTransform;

    [Header("Cindy Movement")]
    public Animator animatorCindy;
    public CindyState currentState;
    public bool canMove = true;
    public float speedMovement;
    private float horizontalInput;
    // private bool isFacingRight;
    // private bool isFacingLeft;

    [Header("Cindy Movement Type")]
    public CindyMovementType cindyMovementType;
    public bool isMoving = false;
    public bool isStepPlaying = false;
    public int intAutoMovement = 0;
    public bool canChangeMovementType = false;
    [Header("Footsteps Audio")]
    public AudioSource audioSourceFootsteps;
    public AudioClip[] footstepSounds;
    public AudioClip[] footstepOnGrass;
    public float stepInterval = 0.5f;

    void Start()
    {
        footstepSounds = footstepOnGrass;
        // Initialize state
        // jika currentState tidak kosong / null
        // if (currentState != null)        
        // {
        //     currentState = CindyState.IdleFacingRight;
        //     UpdateCindyState(currentState);
        // }

        functionMovementCindy.Add(CindyMovementManual);
        functionMovementCindy.Add(CindyAutoMovementRight);
        functionMovementCindy.Add(CindyAutoMovementLeft);
    }

    void FixedUpdate()
    {
        if (cindyLookAtTransform != null)
            CindyLookAtTarget(cindyLookAtTransform);

        if (!canMove)
            return;

        functionMovementCindy[intAutoMovement]();
        // CindyMovementManual();
        
        // jalankan cindy berdsaarkan movement type
        if (!canChangeMovementType)
            return;
        switch (cindyMovementType)
        {
            case CindyMovementType.ManualMovement:
                // functionMovementCindy[0] = CindyMovementManual;
                intAutoMovement = 0;
                break;
            case CindyMovementType.AutoMovementRight:
                // functionMovementCindy[0] = CindyAutoMovementRight;
                intAutoMovement = 1;
                currentState = CindyState.SideFacingRight;
                UpdateCindyState(currentState);
                break;
            case CindyMovementType.AutoMovementLeft:
                // functionMovementCindy[0] = CindyAutoMovementLeft;
                intAutoMovement = 2;
                currentState = CindyState.SideFacingLeft;
                UpdateCindyState(currentState);
                break;
        }
        canChangeMovementType = false;
    }

    public void ChangeMovementType(string directionMove = "Right", bool autoMovement = false, bool walkInPlace = false)
    {
        if (walkInPlace)
        {
            if (directionMove == "Right")
            {
                cindyMovementType = CindyMovementType.AutoMovementRight;
                currentState = CindyState.SideFacingRight;
                intAutoMovement = 1;
            }
            else if (directionMove == "Left")
            {
                cindyMovementType = CindyMovementType.AutoMovementLeft;
                currentState = CindyState.SideFacingLeft;
                intAutoMovement = 2;
            }
        }
        else
        {
            if (autoMovement)
            {
                if (directionMove == "Right")
                {
                    cindyMovementType = CindyMovementType.AutoMovementRight;
                    currentState = CindyState.SideFacingRight;
                    intAutoMovement = 1;
                }
                else if (directionMove == "Left")
                {
                    cindyMovementType = CindyMovementType.AutoMovementLeft;
                    currentState = CindyState.SideFacingLeft;
                    intAutoMovement = 2;
                }
            }
            else
            {
                if (directionMove == "Right")
                    currentState = CindyState.IdleFacingRight;
                else if (directionMove == "Left")
                    currentState = CindyState.IdleFacingLeft;
                cindyMovementType = CindyMovementType.ManualMovement;
                intAutoMovement = 0;
            }
        }
        UpdateCindyState(currentState);
    }

    void CindyAutoMovementRight()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);
    }

    void CindyAutoMovementLeft()
    {
        rb2D.velocity = new Vector2(-speedMovement, rb2D.velocity.y);
    }

    IEnumerator PlayFootstepSound()
    {
        while (isMoving)
        {
            if (!isStepPlaying)
            {
                isStepPlaying = true;
                audioSourceFootsteps.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSourceFootsteps.PlayOneShot(audioSourceFootsteps.clip);
                yield return new WaitForSeconds(stepInterval);
                isStepPlaying = false;
            }
            yield return null;
        }
    }
    void CindyMovementManual()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);

        if (horizontalInput != 0)
        {
            if (Mathf.Abs(horizontalInput) > 0.2f)
            {
                isMoving = true;
                StartCoroutine(PlayFootstepSound());
            }
        }
        else
        {
            isMoving = false;
            StopCoroutine(PlayFootstepSound());
        }

        if (horizontalInput > 0)
        {
            if (currentState != CindyState.SideFacingRight)
            {
                currentState = CindyState.SideFacingRight;
                UpdateCindyState(currentState);
            }
        }
        else if (horizontalInput < 0)
        {
            if (currentState != CindyState.SideFacingLeft)
            {
                currentState = CindyState.SideFacingLeft;
                UpdateCindyState(currentState);
            }
        }
        else
        {
            if (currentState == CindyState.SideFacingRight || currentState == CindyState.IdleFacingRight)
            {
                if (currentState != CindyState.IdleFacingRight)
                {
                    currentState = CindyState.IdleFacingRight;
                    UpdateCindyState(currentState);
                }
            }
            else if (currentState == CindyState.SideFacingLeft || currentState == CindyState.IdleFacingLeft)
            {
                if (currentState != CindyState.IdleFacingLeft)
                {
                    currentState = CindyState.IdleFacingLeft;
                    UpdateCindyState(currentState);
                }
            }
        }
    }

    public void StopMovement()
    {
        isMoving = false;
        rb2D.velocity = Vector2.zero;
        horizontalInput = 0;
        StopCoroutine(PlayFootstepSound());

        if (currentState == CindyState.SideFacingRight || currentState == CindyState.IdleFacingRight)
        {
            if (currentState != CindyState.IdleFacingRight)
            {
                currentState = CindyState.IdleFacingRight;
                UpdateCindyState(currentState);
            }
        }
        else if (currentState == CindyState.SideFacingLeft || currentState == CindyState.IdleFacingLeft)
        {
            if (currentState != CindyState.IdleFacingLeft)
            {
                currentState = CindyState.IdleFacingLeft;
                UpdateCindyState(currentState);
            }
        }
    }

    void UpdateCindyState(CindyState newState)
    {
        cindyFacingRightGO.SetActive(newState == CindyState.SideFacingRight);
        cindyFacingLeftGO.SetActive(newState == CindyState.SideFacingLeft);
        cindyIdleFacingRightGO.SetActive(newState == CindyState.IdleFacingRight);
        cindyIdleFacingLeftGO.SetActive(newState == CindyState.IdleFacingLeft);
    }

    public void CindyLookAtTarget(Transform target)
    {
        if (target.position.x > transform.position.x)
        {
            SetIdleFacingRight();
        }
        else
        {
            SetIdleFacingLeft();
        }
    }

    public void SetFacingRight()
    {
        if (currentState != CindyState.SideFacingRight)
        {
            currentState = CindyState.SideFacingRight;
            UpdateCindyState(currentState);
        }
    }

    public void SetFacingLeft()
    {
        if (currentState != CindyState.SideFacingLeft)
        {
            currentState = CindyState.SideFacingLeft;
            UpdateCindyState(currentState);
        }
    }

    public void SetIdleFacingRight()
    {
        if (currentState != CindyState.IdleFacingRight)
        {
            currentState = CindyState.IdleFacingRight;
            UpdateCindyState(currentState);
        }
    }

    public void SetIdleFacingLeft()
    {
        if (currentState != CindyState.IdleFacingLeft)
        {
            currentState = CindyState.IdleFacingLeft;
            UpdateCindyState(currentState);
        }
    }
}