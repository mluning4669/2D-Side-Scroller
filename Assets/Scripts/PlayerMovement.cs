using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum KingState
{
    PlayerControl,
    Leaving,
    Arriving,
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float cooldown;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask doorLayer;

    public PlayerCombat playerCombat;
    private float timer = 0;
    private BoxCollider2D boxCollider;
    private float horizontalMovement;
    private KingState kingState;
    private PlayerAudio playerAudio;
    

    public void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerAudio = GetComponentInChildren<PlayerAudio>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
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

        anim.SetBool("IsRunning", horizontalMovement != 0 && IsGrounded() && IsPlayerControlled());
        anim.SetBool("IsJumping", IsJumping() && IsPlayerControlled());
        anim.SetBool("IsFalling", IsFalling() && IsPlayerControlled());
    }

    private bool IsPlayerControlled()
    {
        return kingState == KingState.PlayerControl;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return hit.collider != null;
    }

    private bool IsFalling()
    {
        return rb.velocity.y < -0.01f && !IsGrounded();
    }

    private bool IsJumping()
    {
        return rb.velocity.y > 0.01f && !IsGrounded();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && timer <= 0 && IsPlayerControlled())
        {
            playerCombat.Attack();

            timer = cooldown;
        }
    }

    private GameObject AtDoor()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 0.1f, doorLayer);
        return hit.collider != null ? hit.collider.gameObject : null;
    }

    public void ChangeState(KingState newState)
    {
        switch (kingState)
        {
            case KingState.Leaving:
                anim.SetBool("IsLeaving", false);
                break;

            case KingState.Arriving:
                anim.SetBool("IsArriving", false);
                break;
        }

        kingState = newState;

        switch (kingState)
        {
            case KingState.Leaving:
                anim.SetBool("IsLeaving", true);
                break;

            case KingState.Arriving:
                anim.SetBool("IsArriving", true);
                break;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && IsPlayerControlled())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            playerAudio.PlayJumpEffect();
            anim.SetBool("IsJumping", true);
        }
        else if (context.canceled && !IsFalling() && IsPlayerControlled())
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            anim.SetBool("IsJumping", true);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (IsPlayerControlled())
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
        }
    }

    public void Open(InputAction.CallbackContext context)
    {
        if (context.canceled && IsPlayerControlled())
        {
            GameObject door = AtDoor();
            if (door != null)
            {
                DoorScript doorScript = door.GetComponent<DoorScript>();

                doorScript.Open();
                ChangeState(KingState.Leaving);
            }
        }
    }

    public void MoveKingToExit(Transform exit)
    {
        rb.transform.position = exit.position;
    }

    public void Arriving()
    {
        ChangeState(KingState.Arriving);
    }

    public void Arrived()
    {
        ChangeState(KingState.PlayerControl);
    }
}
