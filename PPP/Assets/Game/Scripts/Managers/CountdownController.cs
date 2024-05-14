using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class CountdownController : Singleton<CountdownController>
{
    [SerializeField] private Image im_countdown;
    [SerializeField] private Sprite[] _countdownSprites;
    [SerializeField] private GameObject _countdownPanel;

    private RectTransform rt_imCountDown;
    private AudioManager _audioManager => AudioManager.I;

    protected new void Awake()
    {
        rt_imCountDown = im_countdown.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartCoroutine(CountdownToStart());
    }

    #region Start Countdown
    public void StartCountdown()
    {
        StartCoroutine(CountdownToStart());
    }

    public IEnumerator CountdownToStart()
    {
        im_countdown.enabled = false;
        rt_imCountDown.localScale = Vector2.zero;

        yield return new WaitForSecondsRealtime(Helpers.blackFadeTime + 0.2f);

        im_countdown.enabled = true;

        int i = 0;

        while (i < 3)
        {
            im_countdown.sprite = _countdownSprites[i];
            rt_imCountDown.DOScale(1, 0.5f).OnComplete(() => rt_imCountDown.DOScale(0, 0.5f).SetEase(Ease.InFlash).SetUpdate(true)).SetEase(Ease.OutFlash).SetUpdate(true);
            //_audioManager.PlaySfx("123Countdown");
            yield return new WaitForSecondsRealtime(1f);

            i++;
        }

        //_audioManager.PlaySfx("GOCountdown");

        LevelController.I.BeginLevel();
    }

    #endregion
}