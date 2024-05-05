using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class CountdownController : Singleton<CountdownController>
{
    [SerializeField] private Image im_countdown;
    [SerializeField] private Sprite[] _countdownSprites;
    [SerializeField] private GameObject _countdownPanel;
    [SerializeField] private GameObject _HUDPanel;

    private AudioManager _audioManager => AudioManager.I;

    protected new void Awake()
    {
        
    }

    #region Start Countdown
    public void StartCountdown()
    {
        StartCoroutine(CountdownToStart());
    }

    public IEnumerator CountdownToStart()
    {
        im_countdown.enabled = false;

        yield return new WaitForSeconds(0.5f);

        im_countdown.enabled = true;

        int i = 0;

        while (i < 3)
        {
            im_countdown.sprite = _countdownSprites[i];
            //_audioManager.PlaySfx("123Countdown");
            yield return new WaitForSeconds(1f);

            i++;
        }

        //_audioManager.PlaySfx("GOCountdown");

        LevelController.I.BeginLevel();

        _HUDPanel.SetActive(true);
        _countdownPanel.SetActive(false);

    }

    #endregion

    #region Pause Countdown
    public IEnumerator CountdownPause()
    {
        im_countdown.enabled = false;

        yield return new WaitForSeconds(0.5f);

        im_countdown.enabled = true;

        int i = 0;

        while (i < 3)
        {
            im_countdown.sprite = _countdownSprites[i];
            //_audioManager.PlaySfx("123Countdown");
            yield return new WaitForSecondsRealtime(1f);

            i++;

        }

        //_audioManager.PlaySfx("GOCountdown");

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1;

        LevelController.I.SetLevelState(LevelState.BEGIN);

        _HUDPanel.SetActive(true);
        _countdownPanel.SetActive(false);
    }

    #endregion
}