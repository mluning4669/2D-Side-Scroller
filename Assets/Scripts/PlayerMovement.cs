using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float cooldown;
    [SerializeField] private float jump;
    public PlayerCombat playerCombat;
    private float timer = 0;

    // Update is called once per frame
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Attack") && timer <= 0)
        {
            playerCombat.Attack();

            timer = cooldown;
        }
    }

    public void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        
        //flip player image when moving left and right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetBool("IsJumping", true);
        }


        anim.SetBool("IsRunning", horizontalInput != 0);
        anim.SetBool("IsJumping", rb.velocity.y > 0.01f);
        anim.SetBool("IsFalling", rb.velocity.y < -0.01f);
    }

}
