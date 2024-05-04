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
    [SerializeField] private float speedMovement = 5.0f;
    public int intAutoMovement = 0;
    private List<System.Action> functionMovePlayer = new List<System.Action>();

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // camTransform = Camera.main.transform;
        functionMovePlayer.Add(Movement);
        functionMovePlayer.Add(AutoMovementToRight);
    }
    
    void FixedUpdate()
    {
        if (!canMove)
            return;

        functionMovePlayer[intAutoMovement]();
        // Movement();
        // camTransform.position = new Vector3(transform.position.x, camTransform.position.y, camTransform.position.z);
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
    }

    void AutoMovementToRight()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);
    }
}
