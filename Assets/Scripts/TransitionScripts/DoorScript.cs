using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerArrival;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("IsIdle", true);
    }

    public void Open()
    {
        //player.transform.position = exit.transform.position;
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsOpening", true);
    }

    public void Close()
    {
        player.transform.position = playerArrival.position;
        anim.SetBool("IsOpening", false);
        anim.SetBool("IsClosing", true);
    }

    public void Idle()
    {
        anim.SetBool("IsClosing", false);
        anim.SetBool("IsIdle", true);
        
    }

}
