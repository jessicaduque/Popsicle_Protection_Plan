using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    private PoolManager _poolManager => PoolManager.I;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _poolManager.ReturnPool(collision.gameObject);
    }

}
