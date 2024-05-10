using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private Image im_pauseImage;
    private float _animationTime = 1;

    [SerializeField] Sprite[] _possiblePauseImageSprites;
    [SerializeField] Button[] _buttons;

    BlackScreenController _blackScreenController => BlackScreenController.I;
    private void Awake()
    {
        im_pauseImage = GetComponent<Image>();
    }

    private void Start()
    {
        SetupButtons();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForPanelStart());
    }

    private IEnumerator WaitForPanelStart()
    {
        RandomizePauseImage();
        ResetButtons();

        yield return new WaitForSeconds(Helpers.panelFadeTime);

        ButtonsAnimation();
        
        yield return new WaitForSeconds(_animationTime);

        EnableButtons();
    }

   
    #region
    private void SetupButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            string name = _buttons[i].GetComponent<ButtonExtra>().nameTag;
            switch (name)
            {
                case "resume":
                    _buttons[i].onClick.AddListener(() => _blackScreenController.FadePanel(this.gameObject, false));
                    break;
                case "restart":
                    _buttons[i].onClick.AddListener(() =>_blackScreenController.FadeOutScene("Main"));
                    break;
                case "exit":
                    _buttons[i].onClick.AddListener(() => _blackScreenController.FadeOutScene("MainMenu"));
                    break;
            }
        }
    }

    private void ResetButtons()
    {
        for(int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].enabled = false;
            _buttons[i].gameObject.transform.localScale = Vector3.zero;
        }
    }

    private void EnableButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].enabled = true;
        }
    }

    private void ButtonsAnimation()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].transform.DOScale(1, _animationTime).SetEase(Ease.InBounce);
        }
    }

    #endregion Buttons

    private void RandomizePauseImage()
    {
        im_pauseImage.sprite = _possiblePauseImageSprites[Random.Range(0, _possiblePauseImageSprites.Length)];
    }
}
