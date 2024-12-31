using System;
using UnityEngine;

public class Power : MonoBehaviour
{
    private bool _canUsePower = true;
    
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
