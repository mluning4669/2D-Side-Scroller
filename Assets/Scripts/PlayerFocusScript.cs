using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocusScript : MonoBehaviour
{
    [SerializeField] private Transform activeFocus;
    private Transform playerFocus;

    public void Start()
    {
        playerFocus = GetComponentsInChildren<Transform>()[2];
    }

    public void ResetPlayerFocus()
    {
        playerFocus.position = new Vector2(playerFocus.position.x, activeFocus.position.y);
        Debug.Log("breakpoint");
    }
}
