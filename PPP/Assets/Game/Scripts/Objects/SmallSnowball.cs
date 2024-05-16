using UnityEngine;

public class SmallSnowball : Snowball
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = -Vector2.up * _speed;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        _poolmanager.ReturnPool(this.gameObject);
        _audioManager.PlaySfx("smallsnowballhit");
    }
}
