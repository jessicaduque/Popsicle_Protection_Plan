using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomizeCard : MonoBehaviour
{
    private int _cardsToRandomize = 10;
    private Sprite[] _characterPowerSprites;
    private int _lastRandomPower;
    private int chosenSprite;
    private Image im_this;

    public event Action SpriteRandomizedEvent;
    private void Awake()
    {
        im_this = GetComponent<Image>();
    }
    public void DefinePowerSprites(Sprite[] sprites)
    {
        _characterPowerSprites = sprites;
        _lastRandomPower = UnityEngine.Random.Range(0, _characterPowerSprites.Length);
        ChangeSprite(_characterPowerSprites[_lastRandomPower]);
    }
    public int RandomizeSprite()
    {
        if (_cardsToRandomize == 1)
        {
            return chosenSprite;
        }

        int power = UnityEngine.Random.Range(0, _characterPowerSprites.Length);
        while (power == _lastRandomPower)
        {
            power = UnityEngine.Random.Range(0, _characterPowerSprites.Length);
        }

        if(_cardsToRandomize == 2)
        {
            while(power == chosenSprite)
            {
                power = UnityEngine.Random.Range(0, _characterPowerSprites.Length);
            }
        }

        _lastRandomPower = power;
        return power;
    }

    private void ChangeSprite(Sprite sprite)
    {
        im_this.sprite = sprite;
    }

    public IEnumerator CardRandomization(int chosenSprite)
    {
        this.chosenSprite = chosenSprite;
        float multiplier = 0.2f;
        while (_cardsToRandomize > 0)
        {
            ChangeSprite(_characterPowerSprites[RandomizeSprite()]);
            multiplier += 0.4f;
            if (multiplier > 1.4f)
                multiplier = 1.4f;
            _cardsToRandomize--;
            yield return new WaitForSeconds((3 - multiplier) * 0.1f);
        }
        SpriteRandomizedEvent?.Invoke();
    }
}
