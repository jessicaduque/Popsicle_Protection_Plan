using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerUIShow : MonoBehaviour
{
    
    [SerializeField] private Image im_thisFill;
    [SerializeField] bool _isPenguinBlessing;

    private float _rechargeTime;
    private Image im_thisBackground;
    private Power _thisPower;
    LevelController _levelController => LevelController.I;
    private void Awake()
    {
        im_thisBackground = GetComponent<Image>();
    }

    private void OnEnable()
    {
        im_thisFill.fillAmount = 1;
        if (_isPenguinBlessing)
        {
            SetPowerDetails(_levelController._levelPenguinBlessingSO.power_rechargeTime, _levelController._levelPenguinBlessingSO.power_sprite);
            _thisPower = Player_Penguin_Controller.I.GetPower();
            _thisPower.powerActivatedEvent += () => PowerUsed();
        }
        else
        {
            SetPowerDetails(_levelController._levelPolarBearBlessingSO.power_rechargeTime, _levelController._levelPolarBearBlessingSO.power_sprite);
            _thisPower = Player_Polar_Bear_Controller.I.GetPower();
            _thisPower.powerActivatedEvent += () => PowerUsed();
        }

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void PowerUsed()
    {
        im_thisFill.fillAmount = 0;
        RechargeBlessing();
    }

    private void RechargeBlessing()
    {
        DOTween.To(() => im_thisFill.fillAmount, x => im_thisFill.fillAmount = x, 1, _rechargeTime).OnComplete(() => _thisPower.SetCanUsePower(true));
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