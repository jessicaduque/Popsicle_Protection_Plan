using UnityEngine;

public class Power_PenguinProtection : Power
{
    [SerializeField] private Pool penguinDummy;
    [SerializeField] private int amountPenguinDummies;
    [SerializeField] private Vector2 xPositions;
    [SerializeField] private Vector2 yLimits;
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
        float yLimit = Mathf.Abs((yLimits.x - yLimits.y) / (amountPenguinDummies / 2));
        for (int i = 0; i < amountPenguinDummies / 2; i++)
        {
            Vector2 pos = new Vector2(xPositions[0], i * yLimit + yLimits[1]); 
            Dummy_Penguin_Controller dummy = _poolManager.GetObject("Penguin_Dummies", pos, Quaternion.identity).GetComponent<Dummy_Penguin_Controller>();
            dummy.SetFinalXPos(xPositions[1]);
            dummy.SetDirection(1);
        }
        for (int i = 0; i < amountPenguinDummies / 2; i++)
        {
            Vector2 pos = new Vector2(xPositions[1], i * yLimit + yLimits[1]); 
            Dummy_Penguin_Controller dummy = _poolManager.GetObject("Penguin_Dummies", pos, Quaternion.identity).GetComponent<Dummy_Penguin_Controller>();
            dummy.SetFinalXPos(xPositions[0]);
            dummy.SetDirection(-1);
        }
    }
}
