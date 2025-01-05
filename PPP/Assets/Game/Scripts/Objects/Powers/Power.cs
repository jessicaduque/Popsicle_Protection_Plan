using System;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
    private bool _canUsePower = true;
    protected float UsageTime;
    public event Action powerActivatedEvent;

    public virtual bool UsePower()
    {
        if (!_canUsePower)
            return false;
        
        powerActivatedEvent?.Invoke();
        SetCanUsePower(false);
        return true;
    }

    #region Set

    public void SetCanUsePower(bool state)
    {
        _canUsePower = state;
    }

    #endregion
}
