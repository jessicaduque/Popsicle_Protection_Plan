using System;
using Utils.Singleton;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public event Action beginLevel;
    public event Action timeUp;

    private LevelState _levelState = LevelState.COUNTDOWN;

    protected new void Awake()
    {

    }

    public void BeginLevel()
    {
        beginLevel?.Invoke();
        _levelState = LevelState.BEGIN;
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
}