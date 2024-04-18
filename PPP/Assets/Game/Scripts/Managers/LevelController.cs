using System;
using Utils.Singleton;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public event Action beginLevel;
    public event Action timeUp;

    private LevelState _levelState = LevelState.POWER_DESIGNATION;

    protected new void Awake()
    {

    }

    public void BeginLevel()
    {
        _levelState = LevelState.BEGIN;
        beginLevel?.Invoke();
    }

    public void TimeUp()
    {
        _levelState = LevelState.END;
        timeUp?.Invoke();
    }

    #region Set
    public void SetLevelState(LevelState state)
    {
        _levelState = state;
    }
    #endregion

    #region Get
    public LevelState GetLevelState()
    {
        return _levelState;
    }
    #endregion
}