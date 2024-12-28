using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_PenguinProtection : Power
{
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
