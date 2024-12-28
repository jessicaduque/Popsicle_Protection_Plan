using UnityEngine;
using Utils.Singleton;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BlackScreenCircleEffectController : DontDestroySingleton<BlackScreenCircleEffectController>
{
    [SerializeField] private GameObject _circle;
    private RectTransform _circleRT;

    private float _circleEffectTime = 1.5f;
    private Vector3 _endScale = new Vector3(2500, 2500, 0);

    protected override void Awake()
    {
        base.Awake();

        _circleRT = _circle.GetComponent<RectTransform>();
        _circle.SetActive(false);
    }

    #region Circle Control

    public void CircleScreenOpen()
    {
        _circleRT.sizeDelta = Vector2.zero;
        _circle.SetActive(false);
        _circle.SetActive(true);
        _circleRT.DOSizeDelta(_endScale, _circleEffectTime).SetEase(Ease.InOutBounce).OnComplete(() => _circle.SetActive(false));
    }

    public void CircleScreenClose()
    {
        _circleRT.sizeDelta = _endScale;
        _circle.SetActive(true);
        _circleRT.DOSizeDelta(Vector2.zero, _circleEffectTime).SetEase(Ease.InOutBounce, 1);
    }

    #endregion

    #region Circle Control to Scene

    public void CircleScreenClose(string sceneName)
    {
        _circleRT.sizeDelta = _endScale;
        _circle.SetActive(true);
        _circleRT.DOSizeDelta(Vector2.zero, _circleEffectTime).SetEase(Ease.InOutBounce, 1).SetUpdate(true).OnComplete(() => {
            SceneManager.LoadScene(sceneName);
            });
    }

    #endregion
}
