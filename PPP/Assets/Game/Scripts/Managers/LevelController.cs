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
    public event Action blessingsRandomizedEvent;

    private LevelState _levelState;
    public Power_SO _levelPenguinBlessingSO { get; private set; }
    public Power_SO _levelPolarBearBlessingSO { get; private set; }
    public Power _levelPenguinBlessing { get; private set; }
    public Power _levelPolarBearBlessing { get; private set; }

    private new void Awake()
    {
    }

    #region Blessings Designation 
    public void BeginBlessings()
    {
        //_levelState = LevelState.POWER_DESIGNATION;
        //blessingsEvent?.Invoke();
        BeginCountdown();
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

    #region Set

    public void SetLevelBlessings(Power_SO penguinBlessing, Power_SO polarBearBlessing)
    {
        _levelPenguinBlessingSO = penguinBlessing;
        _levelPolarBearBlessingSO = polarBearBlessing;

        _levelPenguinBlessing = Instantiate(_levelPenguinBlessingSO.power_controllerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Power>();
        _levelPolarBearBlessing = Instantiate(_levelPolarBearBlessingSO.power_controllerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Power>();

        blessingsRandomizedEvent?.Invoke();
    }

    #endregion
}