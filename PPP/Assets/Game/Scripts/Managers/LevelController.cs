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
    private bool _penguinIsWinner = true;
    public Power_SO _levelPenguinBlessingSO { get; private set; }
    public Power_SO _levelPolarBearBlessingSO { get; private set; }
    public Power _levelPenguinBlessing { get; private set; }
    public Power _levelPolarBearBlessing { get; private set; }
    
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
        if (Player_Penguin_Controller.I._isDead || !Player_Penguin_Controller.I._hasPopsicle)
        {
            _penguinIsWinner = false;
        }

        if (Player_Polar_Bear_Controller.I._isDead)
        {
            _penguinIsWinner = true;
        }
        
        timeUpEvent?.Invoke();
    }

    #endregion

    #region Get
    public LevelState GetLevelState()
    {
        return _levelState;
    }

    public bool GetPenguinIsWinner()
    {
        return _penguinIsWinner;
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