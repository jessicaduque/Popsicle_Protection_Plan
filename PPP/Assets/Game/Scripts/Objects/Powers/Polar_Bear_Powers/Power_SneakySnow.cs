using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_SneakySnow : Power
{
    public override void UsePower()
    {
        base.UsePower();

        AttacksInvisible();
    }

    private void AttacksInvisible()
    {
    }
}
