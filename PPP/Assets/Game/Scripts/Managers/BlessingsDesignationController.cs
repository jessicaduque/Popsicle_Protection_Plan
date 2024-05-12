using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BlessingsDesignationController : MonoBehaviour
{
    [SerializeField] RandomizeCard _penguinSpriteRandomizer;
    [SerializeField] RandomizeCard _polarBearSpriteRandomizer;

    [SerializeField] private Power_SO[] _penguinBlessingsArray;
    [SerializeField] private Power_SO[] _polarBearBlessingsArray;

    [SerializeField] private GameObject _penguinSprite;
    [SerializeField] private GameObject _polarBearSprite;

    [SerializeField] private GameObject _penguinBlessingTextBackground;
    [SerializeField] private GameObject _polarBearBlessingTextBackground;

    private int _penguinChosenBlessing;
    private int _polarBearChosenBlessing;

    private void Start()
    {
        _penguinBlessingTextBackground.transform.localScale = Vector3.zero;
        _polarBearBlessingTextBackground.transform.localScale = Vector3.zero;

        _penguinSpriteRandomizer.DefinePowerSprites(GetPowerSprites(_penguinBlessingsArray));
        _polarBearSpriteRandomizer.DefinePowerSprites(GetPowerSprites(_polarBearBlessingsArray));

        _penguinSpriteRandomizer.SpriteRandomizedEvent += () => StartCoroutine(RandomizePowersFinalAnimation());

        _penguinChosenBlessing = Random.Range(0, _penguinBlessingsArray.Length);
        _polarBearChosenBlessing = Random.Range(0, _polarBearBlessingsArray.Length);

        StartCoroutine(RandomizePowersStart());
    }

    private IEnumerator RandomizePowersStart()
    {
        yield return new WaitForSeconds(0.6f + Helpers.blackFadeTime);

        StartCoroutine(_penguinSpriteRandomizer.CardRandomization(_penguinChosenBlessing));
        StartCoroutine(_polarBearSpriteRandomizer.CardRandomization(_polarBearChosenBlessing));
    }

    private IEnumerator RandomizePowersFinalAnimation()
    {
        int animationTime = 4;

        Sequence penguinSeq = DOTween.Sequence();
        penguinSeq.Append(_penguinSprite.transform.DOMoveX(-3000, 2).SetEase(Ease.InOutFlash));
        penguinSeq.Insert(2, _penguinBlessingTextBackground.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBounce));

        Sequence polarSeq = DOTween.Sequence();
        polarSeq.Append(_polarBearSprite.transform.DOMoveX(3000, 2).SetEase(Ease.InOutFlash));
        polarSeq.Insert(1, _polarBearBlessingTextBackground.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBounce));

        yield return new WaitForSeconds(2);
        _penguinSprite.SetActive(false);
        _penguinBlessingTextBackground.GetComponentInChildren<TextMeshProUGUI>().text = _penguinBlessingsArray[_penguinChosenBlessing].power_brief_description;
        _penguinBlessingTextBackground.SetActive(true);

        _polarBearSprite.SetActive(false);
        _polarBearBlessingTextBackground.GetComponentInChildren<TextMeshProUGUI>().text = _polarBearBlessingsArray[_polarBearChosenBlessing].power_brief_description;
        _polarBearBlessingTextBackground.SetActive(true);

        yield return new WaitForSeconds(animationTime - 1);

        yield return new WaitForSeconds(2);

        LevelController.I.BeginCountdown();
    }

    #region Get
    private Sprite[] GetPowerSprites(Power_SO[] powers)
    {
        Sprite[] sprites = new Sprite[powers.Length];
        for(int i = 0; i < powers.Length; i++)
        {
            sprites[i] = powers[i].power_sprite;
        }
        return sprites;
    }
    #endregion
}
