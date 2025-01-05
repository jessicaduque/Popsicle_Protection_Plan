using UnityEngine;

public class Power_PenguinProtection : Power
{
    [SerializeField] private Vector2 minPos, maxPos;
    [SerializeField] private Pool penguinDummy;
    private PoolManager _poolManager => PoolManager.I;
    
    private void Start()
    {
        UsageTime = LevelController.I._levelPenguinBlessingSO.power_useTime;
    }
    
    public override bool UsePower()
    {
        if (base.UsePower())
        {
            Protection();
            return true;
        }
        return false;
    }

    private void Protection()
    {
        
    }
}
