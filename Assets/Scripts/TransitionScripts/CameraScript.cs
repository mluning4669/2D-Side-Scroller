using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform activeFocus;

    public void MoveCameraToRoom(Transform newRoom)
    {
        activeFocus.position = newRoom.position;
    }
}
