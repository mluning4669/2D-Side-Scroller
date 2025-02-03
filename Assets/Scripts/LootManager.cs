using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance {  get; private set; }
    private BigDiamondSpawner _bigDiamondSpawner;
    [SerializeField] private int _bigDiamondsCollectedByPlayer = 0;
    [SerializeField] private Text _bigDiamondText;

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
        _bigDiamondText.text = _bigDiamondsCollectedByPlayer.ToString();
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
