using System;
using Utils.Singleton;

public class LevelController : Singleton<LevelController>
{
    public event Action blessingsEvent;
    public event Action countdownEvent;
    public event Action beginLevelEvent;
    public event Action timeUpEvent;

    private LevelState _levelState;

    private new void Awake()
    {
    }

    #region Blessings Designation 
    public void BeginBlessings()
    {
        _levelState = LevelState.POWER_DESIGNATION;
        blessingsEvent?.Invoke();
    }
    #endregion

    #region Countdown 
    public void BeginCountdown()
    {
        _levelState = LevelState.COUNTDOWN;
        countdownEvent?.Invoke();
    }
    #endregion

    #region Begin Level
    public void BeginLevel()
    {
        _levelState = LevelState.BEGIN;
        beginLevelEvent?.Invoke();
    }
    #endregion

    #region Time Up
    public void TimeUp()
    {
        _levelState = LevelState.END;
        timeUpEvent?.Invoke();
    }

    #endregion

    //#region Set
    //public void SetLevelState(LevelState state)
    //{
    //    _levelState = state;
    //}
    //#endregion

    #region Get
    public LevelState GetLevelState()
    {
        return _levelState;
    }
    #endregion
}