using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_SnowCounter : Power
{
    public override bool UsePower()
    {
        if (base.UsePower())
        {
            Counter();
            return true;
        }
        return false;
    }

    private void Counter()
    {
    }
}
