using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BigDiamondScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask playerLayer;

    private ObjectPool<BigDiamondScript> _pool;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            anim.SetBool("IsHit", true);
        }
    }

    public void DeactivateDiamond()
    {
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<BigDiamondScript> pool)
    {
        _pool = pool;
    }

}
