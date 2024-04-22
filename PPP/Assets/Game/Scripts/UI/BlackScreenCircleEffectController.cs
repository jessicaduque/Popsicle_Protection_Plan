using UnityEngine;
using Utils.Singleton;
using DG.Tweening;

public class BlackScreenCircleEffectController : Singleton<BlackScreenCircleEffectController>
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
        _circle.SetActive(true);
        _circleRT.DOSizeDelta(_endScale, _circleEffectTime).SetEase(Ease.InOutBounce);
    }

    public void CircleScreenClose()
    {
        _circleRT.sizeDelta = _endScale;
        _circle.SetActive(true);
        _circleRT.DOSizeDelta(Vector2.zero, _circleEffectTime).SetEase(Ease.InOutBounce, 1).OnComplete(() => _circle.SetActive(false));
    }

    #endregion
}
