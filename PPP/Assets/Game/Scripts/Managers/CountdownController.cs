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

    private float timeToWait;
    
    private RectTransform rt_imCountDown;
    private AudioManager _audioManager => AudioManager.I;

    protected override void Awake()
    {
        base.Awake();
        
        rt_imCountDown = im_countdown.GetComponent<RectTransform>();
        timeToWait = Helpers.blackFadeTime + 1f;
    }

    private void OnEnable()
    {
        StartCoroutine(CountdownToStartCoroutine());
    }

    #region Start Countdown
    public IEnumerator CountdownToStartCoroutine()
    {
        im_countdown.enabled = false;
        rt_imCountDown.localScale = Vector2.zero;
        yield return new WaitForSecondsRealtime(timeToWait);
        timeToWait = 0; // After first countdown (when game starts), waittime becomes 0, since there isn't any black circle panels to wait for anymore
        im_countdown.enabled = true;

        int i = 0;

        _audioManager.PlaySfx("123countdown");
        while (i < 3)
        {
            im_countdown.sprite = _countdownSprites[i];
            rt_imCountDown.DOScale(1, 0.5f).OnComplete(() => rt_imCountDown.DOScale(0, 0.5f).SetEase(Ease.InFlash).SetUpdate(true)).SetEase(Ease.OutFlash).SetUpdate(true);
            
            yield return new WaitForSecondsRealtime(1f);

            i++;
        }

        LevelController.I.BeginLevel();
    }

    #endregion
}