using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerUIShow : MonoBehaviour
{
    private Image im_thisBackground;
    [SerializeField] private Image im_thisFill;
    [SerializeField] bool _isPenguinBlessing;

    private float _rechargeTime;

    public event Action rechargeDoneEvent;

    LevelController _levelController => LevelController.I;
    private void Awake()
    {
        im_thisBackground = GetComponent<Image>();
    }

    private void Start()
    {
        im_thisFill.fillAmount = 1;
        if (_isPenguinBlessing)
        {
            _levelController.blessingsRandomizedEvent += () => SetPowerDetails(_levelController._levelPenguinBlessing.power_rechargeTime, _levelController._levelPenguinBlessing.power_sprite);
        }
        else
        {
            _levelController.blessingsRandomizedEvent += () => SetPowerDetails(_levelController._levelPolarBearBlessing.power_rechargeTime, _levelController._levelPolarBearBlessing.power_sprite);
        }
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
    private void SetPowerDetails(float rechargeTime, Sprite blessingSprite)
    {
        this._rechargeTime = rechargeTime;
        im_thisFill.sprite = blessingSprite;
        im_thisBackground.sprite = blessingSprite;
    }
    #endregion
}