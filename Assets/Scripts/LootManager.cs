using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance {  get; private set; }
    private BigDiamondSpawner _bigDiamondSpawner;
    private int _bigDiamondsCollectedByPlayer = 0;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        _bigDiamondSpawner = GetComponent<BigDiamondSpawner>();
    }

    public void CollectBigDiamond()
    {
        _bigDiamondsCollectedByPlayer++;
        Debug.Log($"Big diamonds collected: {_bigDiamondsCollectedByPlayer}");
    }

    public void DropBigDiamond(Transform enemyTransform)
    {
        BigDiamondScript script = _bigDiamondSpawner._pool.Get();

        if (script != null) 
        { 
            script.gameObject.transform.position = enemyTransform.position;
        }
    }
}
