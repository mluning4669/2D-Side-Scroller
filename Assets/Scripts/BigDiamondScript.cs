using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDiamondScript : MonoBehaviour
{
    [SerializeField] private GameObject bigDiamond;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask playerLayer;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            anim.SetBool("IsHit", true);
        }
    }

    public void DestroyDiamond()
    {
        bigDiamond.SetActive(false);
    }
}
