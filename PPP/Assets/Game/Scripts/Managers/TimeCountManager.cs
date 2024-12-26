using TMPro;
using UnityEngine;
using System.Collections;
using Utils.Singleton;

public class TimeCountManager : Singleton<TimeCountManager>
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI t_time;

    [Header("Timer Settings")]
    [SerializeField] private float _startTime;
    private float _currentTime;

    private bool _timeOver = false;
    private bool _timerEnding = false;

    private LevelController _levelController => LevelController.I;
    private AudioManager _audioManager => AudioManager.I;

    protected new void Awake() 
    {
        base.Awake();
        
        _currentTime = _startTime + 1;
        SetTimeText();
    }

    private void Start()
    {
        _levelController.beginLevelEvent += delegate { StartCoroutine(StartTimer()); };
        _levelController.pauseEvent += StopAllCoroutines;
    }

    private IEnumerator StartTimer()
    {
        while (!_timeOver)
        {
            _currentTime -= Time.deltaTime;

            if (!_timerEnding && _currentTime <= 6)
            {
                _audioManager.PlaySfx2("clocktick");
                _timerEnding = true;
            }
            else if (_currentTime <= 0)
            {
                _audioManager.StopSfx2();
                _timeOver = true;
            }
            else if (_currentTime > 6)
            {
                _audioManager.StopSfx2();
                _timerEnding = false;
            }

            SetTimeText();
            yield return null;
        }

        TimerEnd();
        _levelController.TimeUp();
    }

    private void TimerEnd()
    {
        _audioManager.StopSfx2();
        _timerEnding = false;
    }

    //private void Update()
    //{
    //    if (_levelController.GetLevelState() == LevelState.BEGIN)
    //    {
    //        if (!_timeOver)
    //        {
    //            _currentTime -= Time.deltaTime;

    //            if (!_timerEnding && _currentTime <= 6)
    //            {
    //                _audioManager.PlaySfx2("clocktick");
    //                _timerEnding = true;
    //            }
    //            else if (_currentTime <= 0)
    //            {
    //                _audioManager.StopSfx2();
    //                _timeOver = true;
    //            }
    //            else if (_currentTime > 6)
    //            {
    //                _audioManager.StopSfx2();
    //                _timerEnding = false;
    //            }

    //            SetTimeText();
    //        }
    //        else
    //        {
    //            _levelController.TimeUp();
    //        }
    //    }
    //    else
    //    {
    //        _audioManager.StopSfx2();
    //        _timerEnding = false;
    //    }

    //}


    #region Set

    public void SetTimeUp()
    {
        _timeOver = true;
    }

    private void SetTimeText()
    {
        int minutes = (int)_currentTime / 60;
        int seconds = (int)_currentTime - minutes * 60;
        t_time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    #endregion
}
