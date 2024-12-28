using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_SneakySnow : Power
{
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
    }
}
