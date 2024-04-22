using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewPortTransform;
    [SerializeField] private RectTransform _contentPanelTransform;
    [SerializeField] private VerticalLayoutGroup _VLG;

    [SerializeField] private RectTransform[] _itemList;
    private Image _changeableSprite;

    private Power_SO[] _characterSOArray;
    private Sprite[] _characterPowerSprites;
    private int _currentSprite = 0;

    private void Start()
    {
        _changeableSprite = _itemList[1].GetComponent<Image>();

        int ItemsToAdd = Mathf.CeilToInt(_viewPortTransform.rect.height / (_itemList[0].rect.height + _VLG.spacing));
        
        for(int i = 0; i < ItemsToAdd; i++)
        {
            RectTransform RT = Instantiate(_itemList[i % _itemList.Length], _contentPanelTransform);
            RT.SetAsLastSibling();
        }

        for(int i = 0; i < ItemsToAdd; i++)
        {
            int num = _itemList.Length - i - 1;
            while(num < 0)
            {
                num += _itemList.Length;
            }
            RectTransform RT = Instantiate(_itemList[num], _contentPanelTransform);
            RT.SetAsFirstSibling();
        }

        _contentPanelTransform.localPosition = new Vector3(_contentPanelTransform.localPosition.x,
            (0 - (_itemList[0].rect.height + _VLG.spacing) * ItemsToAdd),
            _contentPanelTransform.localPosition.z);


        StartCoroutine(ChoosePower());
    }

    //private void Update()
    //{
    //    if (_contentpaneltransform.localposition.y > 0)
    //    {
    //        canvas.forceupdatecanvases();
    //        _contentpaneltransform.localposition -= new vector3(0, _itemlist.length * (_itemlist[0].rect.height + _vlg.spacing), 0);

    //        changesprite();
    //    }

    //    if (_contentpaneltransform.localposition.y < 0 - (_itemlist.length * (_itemlist[0].rect.height + _vlg.spacing)))
    //    {
    //        canvas.forceupdatecanvases();
    //        _contentpaneltransform.localposition += new vector3(0, _itemlist.length * (_itemlist[0].rect.height + _vlg.spacing), 0);

    //        changesprite();
    //    }
    //}

    #region Automatic Scroll

    public IEnumerator ChoosePower()
    {
        yield return new WaitForSeconds(1);

        int chosenPower = Random.Range(0, _characterSOArray.Length);
        Debug.Log("Power chosen: " + chosenPower);

        float time = 0f;

        while(true/*time < 2*/)
        {
            _contentPanelTransform.localPosition += new Vector3(0, 2, 0);

            if (_contentPanelTransform.localPosition.y < 0 - (_itemList.Length * (_itemList[0].rect.height + _VLG.spacing)))
            {
                Canvas.ForceUpdateCanvases();
                _contentPanelTransform.localPosition += new Vector3(0, _itemList.Length * (_itemList[0].rect.height + _VLG.spacing), 0);

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
        for (int i=0; i < _itemList.Length; i++)
        {
            _itemList[0].GetComponent<Image>().sprite = _characterPowerSprites[_currentSprite];
            _currentSprite++;
            if(_currentSprite == _itemList.Length)
            {
                _currentSprite = 0;
            }
        }
    }

    private void ChangeSprite()
    {
        _changeableSprite.sprite = _characterPowerSprites[_currentSprite];
        _currentSprite++;
        if (_currentSprite == _itemList.Length)
        {
            _currentSprite = 0;
        }
    }

    #endregion
}
