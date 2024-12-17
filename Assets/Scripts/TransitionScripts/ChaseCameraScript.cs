using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseCameraScript : MonoBehaviour
{
    [SerializeField] private Transform playerFocus;
    [SerializeField] private Transform roomFocus;
    [SerializeField] private Transform activeFocus;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    public void Start()
    {
        playerFocus.position = new Vector2(playerFocus.position.x, roomFocus.position.y);
    }
    public void FollowPlayer()
    {
        cinemachineCamera.Follow = playerFocus;
    }

    public void ReturnFocusToRoom()
    {
        activeFocus.position = roomFocus.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        FollowPlayer();
        Debug.Log("Triggered!");
    }
}
