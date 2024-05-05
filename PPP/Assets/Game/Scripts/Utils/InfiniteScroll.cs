using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewPortTransform;
    [SerializeField] private RectTransform _contentPanelTransform;
    [SerializeField] private VerticalLayoutGroup _VLG;

    [SerializeField] private RectTransform[] _originalItemList;
    private Image[] _finalItemImageList;

    private Power_SO[] _characterSOArray;
    private Sprite[] _characterPowerSprites;
    private int _currentSprite = 0;
    private int _lastRandomPower;
    private int chosenPower;

    private void Start()
    {
        _lastRandomPower = Random.Range(0, _characterSOArray.Length);
        int ItemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.height / (_originalItemList[0].rect.height + _VLG.spacing));
        
        for(int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform RT = Instantiate(_originalItemList[i % _originalItemList.Length], _contentPanelTransform);
            RT.SetAsLastSibling();
        }

        for(int i = 0; i < ItemsToAdd; i++)
        {
            int num = _originalItemList.Length - i - 1;
            while(num < 0)
            {
                num += _originalItemList.Length;
            }
            RectTransform RT = Instantiate(_originalItemList[num], _contentPanelTransform);
            RT.SetAsFirstSibling();
        }

        _finalItemImageList = new Image[_originalItemList.Length + ItemsToAdd];
        _finalItemImageList = _contentPanelTransform.GetComponentsInChildren<Image>();

        
        _contentPanelTransform.localPosition = new Vector3(_contentPanelTransform.localPosition.x,
            (0 - (_originalItemList[0].rect.height + _VLG.spacing) * ItemsToAdd),
            _contentPanelTransform.localPosition.z);

        Init_DefineSprites();
        StartCoroutine(ChoosePower());
    }

    #region Automatic Scroll

    public IEnumerator ChoosePower()
    {
        yield return new WaitForSeconds(1);

        _finalItemImageList[0].sprite = _characterPowerSprites[_lastRandomPower];

        float time = 0f;

        while(time < 0.8f)
        {
            _contentPanelTransform.localPosition -= new Vector3(0, (0.8f - time) * 200 * Time.deltaTime, 0);

            if (_contentPanelTransform.localPosition.y < 0 - (_originalItemList.Length * (_originalItemList[0].rect.height + _VLG.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                _contentPanelTransform.localPosition += new Vector3(0, _originalItemList.Length * (_originalItemList[0].rect.height + _VLG.spacing), 0);
            }

            time += Time.deltaTime;
            yield return null;
        }

        while(time < 5)
        {
            _contentPanelTransform.localPosition += new Vector3(0, 800 * Time.deltaTime, 0);

            if (_contentPanelTransform.localPosition.y > 0)
            {
                Canvas.ForceUpdateCanvases();
                _contentPanelTransform.localPosition -= new Vector3(0, _originalItemList.Length * (_originalItemList[0].rect.height + _VLG.spacing), 0);

                ChangeSprites();
            }

            time += Time.deltaTime;
            yield return null;
        }

        while(_contentPanelTransform.localPosition.y <= 0)
        {
            _contentPanelTransform.localPosition += new Vector3(0, 800 * Time.deltaTime, 0);
            yield return null;
        }

        Canvas.ForceUpdateCanvases();
        //_contentPanelTransform.localPosition -= new Vector3(0, _originalItemList.Length * (_originalItemList[0].rect.height + _VLG.spacing), 0);

        FinalSprites(chosenPower);


        //while(_currentSprite != chosenPower)
        //{
        //    _contentPanelTransform.localPosition += new Vector3(0, 2, 0);

        //    if (_contentPanelTransform.localPosition.y < 0 - (_itemList.Length * (_itemList[0].rect.height + _VLG.spacing)))
        //    {
        //        Canvas.ForceUpdateCanvases();
        //        _contentPanelTransform.localPosition += new Vector3(0, _itemList.Length * (_itemList[0].rect.height + _VLG.spacing), 0);

        //        ChangeSprite();
        //    }

        //    yield return null;
        //}
    }

    #endregion

    #region Power Sprite Change Control

    public int RandomizePower()
    {
        int power = Random.Range(0, _characterSOArray.Length);
        while(power == _lastRandomPower)
        {
            power = Random.Range(0, _characterSOArray.Length);
        }
        _lastRandomPower = power;
        return power;
    }

    private void Init_DefineSprites()
    {
        for (int i = 0; i < 2 - 1; i++)
        {
            int power = RandomizePower();
            _finalItemImageList[i].sprite = _characterPowerSprites[power];
            _finalItemImageList[i + 1].sprite = _characterPowerSprites[power];
        }
    }

    private void ChangeSprites()
    {
        int newFirstSprite = RandomizePower();
        _currentSprite = newFirstSprite;
        _finalItemImageList[0].sprite = _finalItemImageList[2].sprite;
        _finalItemImageList[1].sprite = _characterPowerSprites[RandomizePower()];
        _finalItemImageList[2].sprite = _characterPowerSprites[newFirstSprite];
    }

    private void FinalSprites(int chosenPower)
    {
        chosenPower = RandomizePower();
        Debug.Log("Escolhido: " + _characterSOArray[chosenPower].power_name);

        if(chosenPower == _currentSprite)
        {
            Debug.Log("entered");
            return;
        }

        _finalItemImageList[0].sprite = _finalItemImageList[2].sprite;
        _finalItemImageList[1].sprite = _characterPowerSprites[chosenPower];
    }

    public void DefinePowersSO
        (Power_SO[] powers)
    {
        _characterSOArray = powers;
        _characterPowerSprites = new Sprite[powers.Length];
        for (int i = 0; i < powers.Length; i++)
        {
            _characterPowerSprites[i] = powers[i].power_sprite;
        }
    }

    #endregion
}
