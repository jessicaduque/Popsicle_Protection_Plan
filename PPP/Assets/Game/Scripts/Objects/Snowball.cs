using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private PoolManager _poolmanager => PoolManager.I;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.forward * _speed;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage(1);
            _poolmanager.ReturnPool(this.gameObject);
        }
    }
}
