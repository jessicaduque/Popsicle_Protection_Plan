using System;
using UnityEngine;
using Utils.Singleton;

public class LevelController : Singleton<LevelController>
{
    public event Action blessingsEvent;
    public event Action countdownEvent;
    public event Action beginLevelEvent;
    public event Action timeUpEvent;
    public event Action pauseEvent;

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
        Time.timeScale = 1;
        _levelState = LevelState.BEGIN;
        beginLevelEvent?.Invoke();
    }
    #endregion

    #region Pause
    public void Pause()
    {
        _levelState = LevelState.PAUSE;
        Time.timeScale = 0;
        pauseEvent?.Invoke();
    }
    #endregion

    #region Time Up
    public void TimeUp()
    {
        _levelState = LevelState.END;
        timeUpEvent?.Invoke();
    }

    #endregion

    #region Get
    public LevelState GetLevelState()
    {
        return _levelState;
    }
    #endregion
}