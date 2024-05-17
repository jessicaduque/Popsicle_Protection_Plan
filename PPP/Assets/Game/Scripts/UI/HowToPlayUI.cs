using UnityEngine;
using DG.Tweening;
using TMPro;

public class HowToPlayUI : MonoBehaviour
{
    [SerializeField] CanvasGroup _firstPageCG;
    [SerializeField] CanvasGroup _secondPageCG;

    [SerializeField] TextMeshProUGUI _pageTitle;
    [SerializeField] TextMeshProUGUI _pageNumber;

    public void GoToFirstPage()
    {
        DOTween.KillAll();
        _secondPageCG.DOFade(0, 0.3f).OnComplete(() => _firstPageCG.DOFade(1, 0.3f));
        _pageTitle.text = "How To Play";
        _pageNumber.text = "1/2";
    }
    public void GoToSecondPage()
    {
        DOTween.KillAll();
        _firstPageCG.DOFade(0, 0.3f).OnComplete(() => _secondPageCG.DOFade(1, 0.3f));
        _pageTitle.text = "How To Win";
        _pageNumber.text = "2/2";
    }
}

