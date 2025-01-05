using System.Collections;
using UnityEngine;

public class Power_SneakySnow : Power
{
    private bool _sneakySnowActivated;

    private void Start()
    {
        UsageTime = LevelController.I._levelPolarBearBlessingSO.power_useTime;

        foreach (SmallSnowball snowball in FindObjectsOfType<SmallSnowball>(true))
        {
            snowball.SetSneakySnowballOn(this);
        }
    }

    public override bool UsePower()
    {
        if (base.UsePower())
        {
            AttacksInvisible();
            return true;
        }
        return false;
    }

    private void AttacksInvisible()
    {
        SetSneakySnowActivated(true);
        StartCoroutine(SneakySnowUsageTimer());
    }

    private IEnumerator SneakySnowUsageTimer()
    {
        yield return new WaitForSeconds(UsageTime);
        SetSneakySnowActivated(false);
    }
    
    private void SetSneakySnowActivated(bool activated)
    {
        _sneakySnowActivated = activated;
    }

    public bool GetSneakySnowActivated()
    {
        return _sneakySnowActivated;
    }
}
