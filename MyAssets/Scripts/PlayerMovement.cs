using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [Header("GameObjects")]
    // public Transform camTransform;
    // [Header("Scripts")]
    // public OpeningGameplay openingGameplay;

    [Header("Player Components")]
    public Rigidbody2D rb2D;

    [Header("Player Movement")]
    public bool canMove = true;
    private float horizontalInput;
    private Vector2 movement;
    public float speedMovement = 5.0f;
    public int intAutoMovement = 0;
    private List<System.Action> functionMovePlayer = new List<System.Action>();

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // camTransform = Camera.main.transform;
        functionMovePlayer.Add(Movement);
        functionMovePlayer.Add(AutoMovementToRight);
        functionMovePlayer.Add(AutoMovementToLeft);
        functionMovePlayer.Add(Movement2);
        functionMovePlayer.Add(Movement3);
        functionMovePlayer.Add(Movement4);
        functionMovePlayer.Add(Movement5);
        Invoke("delayRotation", 1f);
    }
    
    void FixedUpdate()
    {
        if (!canMove)
            return;

        functionMovePlayer[intAutoMovement]();
        
        // Movement();
        // camTransform.position = new Vector3(transform.position.x, camTransform.position.y, camTransform.position.z);
    }

    void delayRotation()
    {
        StartCoroutine(PlayerRotationZ());
    }

    IEnumerator PlayerRotationZ()
    {
        while (transform.rotation.z > 0)
        {
            // Debug.Log(transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.z > 60)
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 50f * Time.deltaTime);
            else if (transform.rotation.eulerAngles.z > 30)
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 40f * Time.deltaTime);
            else if (transform.rotation.eulerAngles.z > 0)
                transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 30f * Time.deltaTime);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        canMove = true;
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
    }

    void Movement2()
    {
        //move position
        horizontalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(horizontalInput, rb2D.velocity.y);
        rb2D.MovePosition(rb2D.position + movement * Time.fixedDeltaTime * speedMovement);
    }

    void Movement3()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(horizontalInput, rb2D.velocity.y);
        rb2D.MovePosition(new Vector2(transform.position.x, transform.position.y) + speedMovement * Time.fixedDeltaTime * movement);
    }

    void Movement4()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(horizontalInput, rb2D.velocity.y);
        rb2D.AddForce(movement * speedMovement * Time.fixedDeltaTime);
    }

    void Movement5()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(horizontalInput, rb2D.velocity.y);
        transform.Translate(movement * speedMovement * Time.deltaTime);
    }

    void AutoMovementToRight()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);
    }

    void AutoMovementToLeft()
    {
        rb2D.velocity = new Vector2(-speedMovement, rb2D.velocity.y);
    }
}
