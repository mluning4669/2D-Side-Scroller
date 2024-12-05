using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float cooldown;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundLayer;

    public PlayerCombat playerCombat;
    private float timer = 0;
    private BoxCollider2D boxCollider;
    private float horizontalMovement;
    

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        //if (Input.GetButtonDown("Attack") && timer <= 0)
        //{
        //    playerCombat.Attack();

        //    timer = cooldown;
        //}
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        
        //flip player image when moving left and right
        if (horizontalMovement > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalMovement < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("IsRunning", horizontalMovement != 0 && IsGrounded());
        anim.SetBool("IsJumping", rb.velocity.y > 0.01f && !IsGrounded());
        anim.SetBool("IsFalling", rb.velocity.y < -0.01f && !IsGrounded());
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return hit.collider != null;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && timer <= 0)
        {
            playerCombat.Attack();

            timer = cooldown;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetBool("IsJumping", true);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
}
