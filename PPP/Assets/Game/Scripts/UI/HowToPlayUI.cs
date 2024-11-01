using UnityEngine;
using DG.Tweening;
using TMPro;

public class HowToPlayUI : MonoBehaviour
{
    [SerializeField] CanvasGroup _firstPageCG;
    [SerializeField] CanvasGroup _secondPageCG;

    [SerializeField] TextMeshProUGUI _pageTitle;
    [SerializeField] TextMeshProUGUI _pageNumber;

    public void ChangePage(bool goToFirst)
    {
        _firstPageCG.DOFade((goToFirst ? 1 : 0), 0.3f);
        _secondPageCG.DOFade((goToFirst ? 0 : 1), 0.3f);

        if (goToFirst)
        {
            _pageTitle.text = "How To Play";
            _pageNumber.text = "1/2";
        }
        else
        {
            _pageTitle.text = "How To Win";
            _pageNumber.text = "2/2";
        }

    }
}

