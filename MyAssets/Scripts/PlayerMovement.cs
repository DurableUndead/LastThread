using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Characters
{
    Alan,
    // Cindy,
    Stalker,
    MirrorPlayer,
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Enum")]
    public Characters character;
    [Space]
    private List<System.Action> functionMovePlayer = new List<System.Action>();
    [Header("Player Components")]
    public Rigidbody2D rb2D;
    public Transform characterBodyTransform;
    public GameObject sideWalkCoreGO;

    [Header("Player Movement")]
    float horizontalInput;
    Vector2 movement;
    public bool canMove = true;
    public float speedMovement = 5.0f;
    public int intAutoMovement = 0;
    
    [Header("Facing Direction")]
    public Vector3 facingRight;
    public Vector3 facingLeft;
    public Vector3 facingIdleRight;
    public Vector3 facingIdleLeft;
    [Header("Audio Footsteps")]
    public bool isMoving;
    public float stepInterval = 0f;
    public bool isStepPlaying;
    public AudioClip[] footstepSounds;
    public AudioClip[] footstepSideWalk;
    public AudioClip[] footstepOnGrass;
    public AudioClip[] footstepWoodenFloor;
    public AudioClip[] footstepLimbo;
    public AudioSource audioSource;
    [Header("Player Animation")]
    public Animator animator;
    private bool isRunning;
    [Header("Sprite Character")]
    public GameObject idleSprite;
    public GameObject walkSprite;
    private float getTransformScaleX;
    // public float getTransformScaleX => idleSprite.transform.localScale.x;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (idleSprite != null)
        {
            getTransformScaleX = idleSprite.transform.localScale.x;
            facingIdleRight = new Vector3(idleSprite.transform.localScale.x, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
            facingIdleLeft = new Vector3(-idleSprite.transform.localScale.x, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
        }

        if (characterBodyTransform != null)
        {
            facingRight = new Vector3(characterBodyTransform.localScale.x, characterBodyTransform.localScale.y, characterBodyTransform.localScale.z);
            facingLeft = new Vector3(-characterBodyTransform.localScale.x, characterBodyTransform.localScale.y, characterBodyTransform.localScale.z);

            // facingIdleRight = new Vector3(idleSprite.transform.localScale.x, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
            // facingIdleLeft = new Vector3(-idleSprite.transform.localScale.x, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
        }

        switch (character)
        {
            case Characters.Alan:
                functionMovePlayer.Add(Movement);
                functionMovePlayer.Add(AutoMovementToRight);
                functionMovePlayer.Add(AutoMovementToLeft);
                break;
            // case Characters.Cindy:
            //     functionMovePlayer.Add(CindyMovementWithWatch);
            //     functionMovePlayer.Add(AutoMovementRightWithoutSprite);
            //     functionMovePlayer.Add(AutoMovementLeftWithoutSprite);
            //     break;
            case Characters.Stalker:
                functionMovePlayer.Add(AutoMovementRightWithoutSprite);
                break;
            case Characters.MirrorPlayer:
                functionMovePlayer.Add(MovementWithoutFootstep);
                break;
        }
        
    }
    
    void FixedUpdate()
    {
        if (!canMove)
            return;

        functionMovePlayer[intAutoMovement]();

        // if(animator != null) // hapus jika cindy punya animator
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
        
        if (horizontalInput != 0)
        {
            characterBodyTransform.localScale = horizontalInput > 0 ? facingRight : facingLeft;
            if (idleSprite.activeSelf)
            {
                idleSprite.SetActive(false);
                walkSprite.SetActive(true);
            }
            if (!isMoving)
            {
                // isMoving = true;
                // if magnitudo > 0.2f
                // if (Mathf.Abs(rb2D.velocity.x) > 0.2f)
                if (Mathf.Abs(horizontalInput) > 0.2f)
                {
                    isMoving = true;
                    StartCoroutine(PlayFootstepSound());
                }
            }
        }
        else
        {
            if (!idleSprite.activeSelf)
            {
                idleSprite.SetActive(true);
                walkSprite.SetActive(false);
                float minOrMax = characterBodyTransform.localScale.x < 0 ? -getTransformScaleX : getTransformScaleX;
                idleSprite.transform.localScale = new Vector3(minOrMax, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
            }

            isMoving = false;
            StopCoroutine(PlayFootstepSound());
            // audioSource.Stop();
            // audioSource.clip = null;
        }

        UpdateAnimation();
    }

    IEnumerator PlayFootstepSound()
    {
        while (isMoving)
        {
            if (!isStepPlaying)
            {
                isStepPlaying = true;
                audioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                // audioSource.Play();
                audioSource.PlayOneShot(audioSource.clip);
                // yield return new WaitForSeconds(audioSource.clip.length + stepInterval); // Tunggu hingga klip selesai ditambah jeda sedikit
                yield return new WaitForSeconds(stepInterval);
                isStepPlaying = false;
            }
            yield return null;
        }
    }


    void UpdateAnimation()
    {
        bool currentlyRunning  = Mathf.Abs(horizontalInput) > 0.1f;

        if (currentlyRunning) //&& !isRunning
        {
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            // isRunning = true;
        }
        else if (!currentlyRunning)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Walk");
            // isRunning = false;
        }
    }

    public void StopWalkingOrRunning(bool mirrorDirection = false, bool changeDirection = false, string targetDirection = "Right")
    {
        isMoving = false;
        StopCoroutine(PlayFootstepSound());
        // audioSource.Stop();
        // audioSource.clip = null;
        rb2D.velocity = Vector2.zero;
        horizontalInput = 0;
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Idle");

        walkSprite.SetActive(false);
        idleSprite.SetActive(true);

        if (changeDirection)
        {
            if (targetDirection == "Right")
                characterBodyTransform.localScale = facingRight;
            else if (targetDirection == "Left")
                characterBodyTransform.localScale = facingLeft;
            idleSprite.transform.localScale = new Vector3(characterBodyTransform.localScale.x, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
            return;
        }

        if (mirrorDirection)
        {
            float minOrMax = characterBodyTransform.localScale.x < 0 ? getTransformScaleX : -getTransformScaleX;
            idleSprite.transform.localScale = new Vector3(minOrMax, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
        }
        else
        {
            float minOrMax = characterBodyTransform.localScale.x < 0 ? -getTransformScaleX : getTransformScaleX;
            idleSprite.transform.localScale = new Vector3(minOrMax, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
        }
    }

    public void LookAtTarget(Transform targetTransform)
    {
        if (targetTransform.position.x > transform.position.x)
        {
            idleSprite.transform.localScale = facingIdleRight;
        }
        else if (targetTransform.position.x < transform.position.x)
        {
            idleSprite.transform.localScale = facingIdleLeft;
        }

        
        // if (targetTransform.position.x > transform.position.x)
        // {
        //     characterBodyTransform.localScale = facingRight;
        // }
        // else if (targetTransform.position.x < transform.position.x)
        // {
        //     characterBodyTransform.localScale = facingLeft;
        // }
        // idleSprite.transform.localScale = new Vector3(characterBodyTransform.localScale.x, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
    }

    void AutoMovementToRight()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);

        if (idleSprite.activeSelf)
        {
            idleSprite.SetActive(false);
            walkSprite.SetActive(true);
            characterBodyTransform.localScale = facingRight;
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk");
        }
    }

    public void AutoMovementToLeft()
    {
        rb2D.velocity = new Vector2(-speedMovement, rb2D.velocity.y);
        if (idleSprite.activeSelf)
        {
            idleSprite.SetActive(false);
            walkSprite.SetActive(true);
            characterBodyTransform.localScale = facingLeft;
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk");
        }
    }

    public void WalkInPlace(string targetDirection)
    {
        if (idleSprite.activeSelf)
        {
            idleSprite.SetActive(false);
            walkSprite.SetActive(true);
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk");

            if (targetDirection == "Right")
                characterBodyTransform.localScale = facingRight;
            else if (targetDirection == "Left")
                characterBodyTransform.localScale = facingLeft;
            // float minOrMax = characterBodyTransform.localScale.x < 0 ? -getTransformScaleX : getTransformScaleX;
            // walkSprite.transform.localScale = new Vector3(minOrMax, walkSprite.transform.localScale.y, walkSprite.transform.localScale.z);
        }
    }

    void MovementWithoutSprite()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
    }

    void MovementWithoutFootstep()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
        
        if (horizontalInput != 0)
        {
            characterBodyTransform.localScale = horizontalInput > 0 ? facingRight : facingLeft;
            if (idleSprite.activeSelf)
            {
                idleSprite.SetActive(false);
                walkSprite.SetActive(true);
            }
        }
        else
        {
            if (!idleSprite.activeSelf)
            {
                idleSprite.SetActive(true);
                walkSprite.SetActive(false);
                float minOrMax = characterBodyTransform.localScale.x < 0 ? -getTransformScaleX : getTransformScaleX;
                idleSprite.transform.localScale = new Vector3(minOrMax, idleSprite.transform.localScale.y, idleSprite.transform.localScale.z);
            }
        }

        UpdateAnimation();
    }

    void AutoMovementLeftWithoutSprite()
    {
        rb2D.velocity = new Vector2(-speedMovement, rb2D.velocity.y);
    }

    void AutoMovementRightWithoutSprite()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);
    }
}