using DG.Tweening;
using UnityEngine;

public class SmallSnowball : Snowball
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    // Sneaky Snow power related variables
    private bool _powerIsSneakySnow = false;
    private Power_SneakySnow _powerSnow;
    private Tween _tweenFlickering;
    private float _flickerTimeSeconds = 0.4f;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = -Vector2.up * _speed;
        
        if (_powerIsSneakySnow)
        {
            _spriteRenderer.color = new Color(255, 255, 255, 1);
            
            if (_powerSnow.GetSneakySnowActivated())
            {
                _tweenFlickering = _spriteRenderer.DOFade(0, _flickerTimeSeconds).SetLoops(-1, LoopType.Yoyo);
            }
        }
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;

        _tweenFlickering.Kill();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
            _audioManager.PlaySfx("smallsnowballhit");
    }

    public void SetSneakySnowballOn(Power_SneakySnow sneakySnow)
    {
        _powerSnow = sneakySnow;
        _powerIsSneakySnow = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
}
