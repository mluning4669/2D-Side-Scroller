using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseCameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform roomFocus;
    [SerializeField] private Transform activeFocus;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] private LayerMask roomFocusLayer;

    private BoxCollider2D boxCollider;
    

    public void Start()
    {
        boxCollider = player.GetComponent<BoxCollider2D>();
    }
    public void FollowPlayer()
    {
        cinemachineCamera.Follow = player.transform;
    }

    public void ReturnFocusToRoom()
    {
        cinemachineCamera.Follow = activeFocus;
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        FollowPlayer();
        Debug.Log("Trigger Entered");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exited");
        GameObject roomFocus = AtRoomFocus();

        if (roomFocus != null) 
        {
            activeFocus.position = roomFocus.transform.position;
            ReturnFocusToRoom();
        }

    }

    public GameObject AtRoomFocus()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 5, roomFocusLayer);
        return hit.collider != null ? hit.collider.gameObject : null;
    }
}
