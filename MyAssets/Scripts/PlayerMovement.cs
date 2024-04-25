using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [Header("GameObjects")]
    // public Transform camTransform;
    [Header("Scripts")]
    public OpeningGameplay openingGameplay;

    [Header("Player Components")]
    private Rigidbody2D rb2D;

    [Header("Player Movement")]
    private float horizontalInput;
    [SerializeField] private float speedMovement = 5.0f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // camTransform = Camera.main.transform;
    }
    
    void FixedUpdate()
    {
        if (!openingGameplay.canPlayGame)
            return;

        Movement();
        // camTransform.position = new Vector3(transform.position.x, camTransform.position.y, camTransform.position.z);
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(horizontalInput * speedMovement, rb2D.velocity.y);
    }
}
