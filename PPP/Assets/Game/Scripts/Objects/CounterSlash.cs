using UnityEngine;

public class CounterSlash : MonoBehaviour
{
    private Vector2 _originalPos;
    private Rigidbody2D _rb;
    private Vector2 _slashForce = new Vector2(0, 4f);
    private void Awake()
    {
        _originalPos = transform.localPosition;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.AddForce(_slashForce, ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
        transform.localPosition = _originalPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SmallSnowball snowball = collision.gameObject.GetComponent<SmallSnowball>();
        if (snowball != null)
        {
            snowball.SetSnowBallVelocity(true);
        }
    }
}
