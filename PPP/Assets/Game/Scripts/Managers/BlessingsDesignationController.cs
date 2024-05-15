using System.Collections;
using UnityEngine;
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

    LevelController _levelController => LevelController.I;

    private void Start()
    {
        _penguinBlessingTextBackground.transform.localScale = Vector3.zero;
        _polarBearBlessingTextBackground.transform.localScale = Vector3.zero;

        _penguinSpriteRandomizer.DefinePowerSprites(GetPowerSprites(_penguinBlessingsArray));
        _polarBearSpriteRandomizer.DefinePowerSprites(GetPowerSprites(_polarBearBlessingsArray));

        _penguinSpriteRandomizer.SpriteRandomizedEvent += () => StartCoroutine(RandomizePowersFinalAnimation());

        _penguinChosenBlessing = Random.Range(0, _penguinBlessingsArray.Length);
        _polarBearChosenBlessing = Random.Range(0, _polarBearBlessingsArray.Length);

        _levelController.SetLevelBlessings(_penguinBlessingsArray[_penguinChosenBlessing], _polarBearBlessingsArray[_polarBearChosenBlessing]);

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
        float initialTime = 1;

        Sequence penguinSeq = DOTween.Sequence();
        penguinSeq.Append(_penguinSprite.transform.DOMoveX(_penguinSprite.transform.position.x - 1000, 2).SetEase(Ease.InOutCirc));
        penguinSeq.Insert(initialTime, _penguinBlessingTextBackground.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutCirc));

        Sequence polarSeq = DOTween.Sequence();
        polarSeq.Append(_polarBearSprite.transform.DOMoveX(_polarBearSprite.transform.position.x + 1000, 2).SetEase(Ease.InOutCirc));
        polarSeq.Insert(initialTime, _polarBearBlessingTextBackground.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutCirc));

        yield return new WaitForSeconds(initialTime);
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
