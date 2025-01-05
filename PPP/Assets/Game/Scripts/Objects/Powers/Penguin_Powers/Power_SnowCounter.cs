using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Power_SnowCounter : Power
{
    private GameObject _counterSlash;
    private SpriteRenderer _counterSlashRenderer;

    private AudioManager _audioManager => AudioManager.I;

    private void Start()
    {
        _counterSlash = Player_Penguin_Controller.I.gameObject.transform.Find("CounterSlash").gameObject;
        _counterSlashRenderer = _counterSlash.GetComponent<SpriteRenderer>();
        UsageTime = LevelController.I._levelPenguinBlessingSO.power_useTime;
    }

    public override bool UsePower()
    {
        if (base.UsePower())
        {
            Counter();
            return true;
        }
        return false;
    }

    private void Counter()
    {
        _audioManager.PlaySfx("counter");

        _counterSlashRenderer.color = new Color(255, 255, 255, 1);
        _counterSlash.SetActive(true);
        StartCoroutine(CounterTimer());
    }

    private IEnumerator CounterTimer()
    {
        yield return new WaitForSeconds(UsageTime);
        
        _counterSlashRenderer.DOFade(0, 0.2f).OnComplete(delegate { _counterSlash.SetActive(false); });
    }
}
