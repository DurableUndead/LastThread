using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerMovement : MonoBehaviour
{
    [Header("Stalker Components")]
    private Rigidbody2D rb2D;

    [Header("Stalker Movement")]
    public bool canMove = true;
    [SerializeField] float speedMovement = 5.0f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (!canMove)
            return;

        AutoMovementToRight();
    }

    void AutoMovementToRight()
    {
        rb2D.velocity = new Vector2(speedMovement, rb2D.velocity.y);
    }
}
