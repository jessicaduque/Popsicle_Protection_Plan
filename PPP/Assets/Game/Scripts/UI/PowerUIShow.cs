using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerUIShow : MonoBehaviour
{
    [SerializeField] private Image im_thisFill;

    private float _rechargeTime;

    public event Action rechargeDoneEvent;

    private void Start()
    {
        im_thisFill.fillAmount = 1;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void PowerUsed()
    {
        im_thisFill.fillAmount = 0;

        StartCoroutine(RechargeBlessing());
    }

    private IEnumerator RechargeBlessing()
    {
        DOTween.To(() => im_thisFill.fillAmount, x => im_thisFill.fillAmount = x, 1, _rechargeTime);
        yield return new WaitForSeconds(_rechargeTime);
        rechargeDoneEvent?.Invoke();
    }

    #region Set
    public void SetPowerDetails(float rechargeTime, Sprite blessingSprite)
    {
        this._rechargeTime = rechargeTime;
        im_thisFill.sprite = blessingSprite;
    }
    #endregion
}