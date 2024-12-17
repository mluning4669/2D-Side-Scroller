using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraScript : MonoBehaviour
{
    [SerializeField] private Transform activeFocus;

    public void MoveCameraToRoom(Transform newRoom)
    {
        activeFocus.position = newRoom.position;
    }
}
