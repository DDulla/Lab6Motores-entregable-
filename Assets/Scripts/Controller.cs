using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float velocity;
    public Vector2 direction;
    public float jumpForce;
    public int maxJumps;
    private int remainingJumps;
    public LayerMask floorLayer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        remainingJumps = maxJumps;
    }
    void Update()
    {
    }
    bool onTheFloor()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, floorLayer);
        return raycastHit.collider != null;
    }
    public void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        rigidBody.velocity = direction * velocity;
    }
    public void Jumps(InputAction.CallbackContext context)
    {
        float inputJump = context.ReadValue<float>();
        if (onTheFloor())
        {
            remainingJumps = maxJumps;
        }
        if (context.performed && remainingJumps > 0)
        {
            remainingJumps = remainingJumps - 1;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
