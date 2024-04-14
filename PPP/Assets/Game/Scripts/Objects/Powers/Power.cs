using UnityEngine;
using UnityEngine.Events;

public class Power : MonoBehaviour
{
    private bool _canUsePower;
    
    public UnityAction powerActivatedEvent;

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
