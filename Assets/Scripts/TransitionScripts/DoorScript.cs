using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerArrival;
    [SerializeField] private Transform roomFocus;

    private Animator exitAnim;
    private Transform exitRoomFocus;

    public void Start()
    {
        anim.SetBool("IsIdle", true);
        exitAnim = exit.GetComponent<Animator>();
        exitRoomFocus = exit.GetComponent<DoorScript>().roomFocus;
    }

    public void Open()
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsOpening", true);
    }

    public void ReturnToIdleFromIsOpening()
    {
        anim.SetBool("IsOpening", false);
        anim.SetBool("IsIdle", true);
    }

    public void ReturnToIdleFromExitIsOpening()
    {
        anim.SetBool("ExitIsOpening", false);
        anim.SetBool("IsIdle", true);
    }

    public void OpenExit()
    {
        exitAnim.SetBool("IsIdle", false);
        exitAnim.SetBool("ExitIsOpening", true);
    }

    public void MoveKingToExit()
    {
        player.GetComponent<PlayerMovement>().MoveKingToExit(playerArrival);
    }

    public void MoveCameraToExitRoom()
    {
        gameObject.GetComponent<RoomCameraScript>().MoveCameraToRoom(exitRoomFocus);
    }
}
