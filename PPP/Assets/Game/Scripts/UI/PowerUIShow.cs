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

    private void Start()
    {
        im_thisFill.fillAmount = 1;
        if (_isPenguinBlessing)
        {
            _levelController.blessingsRandomizedEvent += () =>
            {
                _thisPower = Player_Penguin_Controller.I.GetPower();
                _thisPower.powerActivatedEvent += () => PowerUsed();
                SetPowerDetails(_levelController._levelPenguinBlessingSO.power_rechargeTime, _levelController._levelPenguinBlessingSO.power_sprite);
            };
        }
        else
        {
            _levelController.blessingsRandomizedEvent += () =>
            {
                _thisPower = Player_Polar_Bear_Controller.I.GetPower();
                _thisPower.powerActivatedEvent += () => PowerUsed();
                _levelController.blessingsRandomizedEvent += () => SetPowerDetails(_levelController._levelPolarBearBlessingSO.power_rechargeTime, _levelController._levelPolarBearBlessingSO.power_sprite);
            };
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
        _thisPower.SetCanUsePower(true);
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