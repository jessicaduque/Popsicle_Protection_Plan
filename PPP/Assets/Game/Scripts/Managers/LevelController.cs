using System;
using Utils.Singleton;

public class LevelController : Singleton<LevelController>
{
    public event Action beginLevel;
    public event Action timeUp;

    private LevelState _levelState = LevelState.POWER_DESIGNATION;

    private new void Awake()
    {
    }

    #region Begin Level
    public void BeginLevel()
    {
        _levelState = LevelState.BEGIN;
        beginLevel?.Invoke();
    }
    #endregion

    #region Time Up
    public void TimeUp()
    {
        _levelState = LevelState.END;
        timeUp?.Invoke();
    }

    #endregion

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