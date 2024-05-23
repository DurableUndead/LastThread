using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private List<System.Action> functionMovePlayer = new List<System.Action>();
    [Header("Player Components")]
    public Rigidbody2D rb2D;
    public Transform characterBodyTransform;

    [Header("Player Movement")]
    float horizontalInput;
    Vector2 movement;
    public bool canMove = true;
    public float speedMovement = 5.0f;
    public int intAutoMovement = 0;
    
    [Header("Facing Direction")]
    Vector3 facingRight;
    Vector3 facingLeft;
    [Header("Audio Footsteps")]
    public bool isMoving;
    // public float stepInterval = 0.5f;
    public bool isStepPlaying;
    public AudioClip[] footstepSounds;
    public AudioSource audioSource;
    [Header("Player Animation")]
    public Animator animator;
    private bool isRunning;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();

        facingRight = new Vector3(characterBodyTransform.localScale.x, characterBodyTransform.localScale.y, characterBodyTransform.localScale.z);
        facingLeft = new Vector3(-characterBodyTransform.localScale.x, characterBodyTransform.localScale.y, characterBodyTransform.localScale.z);

        functionMovePlayer.Add(Movement);
        functionMovePlayer.Add(AutoMovementToRight);
        functionMovePlayer.Add(AutoMovementToLeft);
    }
    
    void FixedUpdate()
    {
        if (!canMove)
            return;

        functionMovePlayer[intAutoMovement]();

        if(animator != null)
            UpdateAnimation();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
        
        if (horizontalInput != 0)
        {
            characterBodyTransform.localScale = horizontalInput > 0 ? facingRight : facingLeft;
        
            if (!isMoving)
            {
                isMoving = true;
                StartCoroutine(PlayFootstepSound());
            }
        }
        else
        {
            isMoving = false;
            StopCoroutine(PlayFootstepSound());
            audioSource.Stop();
            audioSource.clip = null;
        }
    }

    IEnumerator PlayFootstepSound()
    {
        while (isMoving)
        {
            if (!isStepPlaying)
            {
                isStepPlaying = true;
                audioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length); // Tunggu hingga klip selesai ditambah jeda sedikit
                isStepPlaying = false;
            }
            yield return null;
        }
    }


    void UpdateAnimation()
    {
        bool currentlyRunning  = Mathf.Abs(horizontalInput) > 0.1f;

        if (currentlyRunning && !isRunning)
        {
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            isRunning = true;
        }
        else if (!currentlyRunning && isRunning)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("Walk");
            isRunning = false;
        }
    }

    void AutoMovementToRight()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);
    }

    void AutoMovementToLeft()
    {
        rb2D.velocity = new Vector2(-speedMovement, rb2D.velocity.y);
    }
    // void Movement2()
    // {
    //     //move position
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     movement = new Vector2(horizontalInput, rb2D.velocity.y);
    //     rb2D.MovePosition(rb2D.position + movement * Time.fixedDeltaTime * speedMovement);
    // }

    // void Movement3()
    // {
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     movement = new Vector2(horizontalInput, rb2D.velocity.y);
    //     rb2D.MovePosition(new Vector2(transform.position.x, transform.position.y) + speedMovement * Time.fixedDeltaTime * movement);
    // }

    // void Movement4()
    // {
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     movement = new Vector2(horizontalInput, rb2D.velocity.y);
    //     rb2D.AddForce(movement * speedMovement * Time.fixedDeltaTime);
    // }

    // void Movement5()
    // {
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     movement = new Vector2(horizontalInput, rb2D.velocity.y);
    //     transform.Translate(movement * speedMovement * Time.deltaTime);
    // }
}
