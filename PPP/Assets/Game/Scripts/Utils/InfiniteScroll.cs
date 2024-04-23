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
    private Image _changeableSprite;
    private Image[] _finalItemImageList;

    private Power_SO[] _characterSOArray;
    private Sprite[] _characterPowerSprites;
    private int _currentSprite = 0;

    private void Start()
    {
        _changeableSprite = _originalItemList[1].GetComponent<Image>();

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


        StartCoroutine(ChoosePower());
    }

    #region Automatic Scroll

    public IEnumerator ChoosePower()
    {
        yield return new WaitForSeconds(1);

        int chosenPower = Random.Range(0, _characterSOArray.Length);
        Debug.Log("Power chosen: " + chosenPower);

        float time = 0f;

        while(true/*time < 2*/)
        {
            _contentPanelTransform.localPosition += new Vector3(0, 4, 0);

            if (_contentPanelTransform.localPosition.y > 0)
            {
                Debug.Log(_contentPanelTransform.localPosition.y);
                Canvas.ForceUpdateCanvases();
                _contentPanelTransform.localPosition -= new Vector3(0, _originalItemList.Length * (_originalItemList[0].rect.height + _VLG.spacing), 0);

                ChangeSprite();
            }

            time += Time.deltaTime;
            yield return null;
        }

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

    public void DefinePowers(Power_SO[] powers)
    {
        _characterSOArray = powers;
        _characterPowerSprites = new Sprite[powers.Length];
        for (int i = 0; i < powers.Length; i++)
        {
            _characterPowerSprites[i] = powers[i].power_sprite;
        }

        DefineInicialSprites();
    }

    private void DefineInicialSprites()
    {
        for (int i=0; i < _originalItemList.Length; i++)
        {
            _originalItemList[0].GetComponent<Image>().sprite = _characterPowerSprites[_currentSprite];
            _currentSprite++;
            if(_currentSprite == _originalItemList.Length)
            {
                _currentSprite = 0;
            }
        }
    }

    private void ChangeSprite()
    {
        _changeableSprite.sprite = _characterPowerSprites[_currentSprite];
        _currentSprite++;
        if (_currentSprite == _originalItemList.Length)
        {
            _currentSprite = 0;
        }
    }

    #endregion
}
