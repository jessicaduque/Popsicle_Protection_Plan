using System;
using UnityEngine;

public class Power : MonoBehaviour
{
    private bool _canUsePower = true;
    
    public event Action powerActivatedEvent;

    public virtual void UsePower()
    {
        if (!_canUsePower)
            return;

        powerActivatedEvent?.Invoke();
        SetCanUsePower(false);
    }

    #region Set

    public void SetCanUsePower(bool state)
    {
        _canUsePower = state;
    }

    #endregion
}
