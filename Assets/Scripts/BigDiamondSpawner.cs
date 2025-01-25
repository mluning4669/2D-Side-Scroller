using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BigDiamondSpawner : MonoBehaviour
{
    public ObjectPool<BigDiamondScript> _pool;
    public BigDiamondScript _diamond;
    
    void Start()
    {
        _pool = new ObjectPool<BigDiamondScript>(Create, OnTakeBigDiamondFromPool, OnReturnBigDiamondToPool, OnDestroyBigDiamond, true, 25, 50);
    }


    public BigDiamondScript Create()
    {
        //spawn new instance of the big diamond
        BigDiamondScript diamond = Instantiate(_diamond);

        diamond.SetPool(_pool);

        return diamond;
    }

    private void OnTakeBigDiamondFromPool(BigDiamondScript diamond)
    {
        diamond.gameObject.SetActive(true);
    }

    private void OnReturnBigDiamondToPool(BigDiamondScript diamond)
    {
        diamond.gameObject.SetActive(false);
    }

    private void OnDestroyBigDiamond(BigDiamondScript diamond)
    {
        Destroy(diamond);
    }
    
}
