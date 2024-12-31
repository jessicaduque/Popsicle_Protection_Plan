using UnityEngine;

public class Snowball : MonoBehaviour
{
    protected PoolManager _poolmanager => PoolManager.I;
    protected AudioManager _audioManager => AudioManager.I;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage(1);
            _poolmanager.ReturnPool(this.gameObject);
        }
    }
}
