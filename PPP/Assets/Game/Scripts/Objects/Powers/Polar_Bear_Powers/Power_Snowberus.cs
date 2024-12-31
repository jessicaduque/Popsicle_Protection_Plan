using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Power_Snowberus : Power
{
    [SerializeField] private ShadowPoints[] _snowFallShadows;
    [SerializeField] private Pool _bigSnowballPoolItem;
    [SerializeField] private float _bigSnowballSpawnY;

    private float _timeBetweenSnowballs = 1f;
    private float _timeForShadowToAppear = 3f;
    private float _timeForSnowballFall = 0.4f;

    private AudioManager _audioManager => AudioManager.I;
    private PoolManager _poolManager => PoolManager.I;
    public override bool UsePower()
    {
        if (base.UsePower())
        {
            SnowFall();
            return true;
        }
        return false;
    }

    private void SnowFall()
    {
        int shadowsArray = Random.Range(0, _snowFallShadows.Length);
        Vector2 snowBallSpawnPoint;
        
        
        for (int i = 0; i < 3; i++)
        {
            snowBallSpawnPoint.x = _snowFallShadows[shadowsArray].shadowPoints[i].transform.position.x;
            snowBallSpawnPoint.y = _bigSnowballSpawnY + _snowFallShadows[shadowsArray].shadowPoints[i].transform.position.y;
            SpriteRenderer shadowSpriteRenderer = _snowFallShadows[shadowsArray].shadowPoints[i].GetComponent<SpriteRenderer>();
            Transform shadowTransform = shadowSpriteRenderer.transform;
            shadowTransform.localScale = Vector3.zero;
            
            shadowSpriteRenderer.DOFade(1, _timeForShadowToAppear).SetDelay(i * _timeBetweenSnowballs);
            shadowTransform.DOScale(Vector3.one * 4, _timeForShadowToAppear).SetDelay(i * _timeBetweenSnowballs);
            GameObject bigSnowball = _poolManager.GetObject(_bigSnowballPoolItem.tagPool, snowBallSpawnPoint, Quaternion.identity);
            bigSnowball.transform.DOMoveY(_snowFallShadows[shadowsArray].shadowPoints[i].transform.position.y, _timeForSnowballFall).SetDelay((i * _timeBetweenSnowballs) + (_timeForShadowToAppear - _timeForSnowballFall)).OnComplete(() =>
            {
                shadowSpriteRenderer.color = new Color(255, 255, 255, 0);
                CapsuleCollider2D collider = shadowSpriteRenderer.GetComponent<CapsuleCollider2D>();
                collider.enabled = true;
                StartCoroutine(ColliderCooldown(collider));
                bigSnowball.GetComponent<Animator>().SetTrigger("Break");
                _audioManager.PlaySfx("bigsnowballhit");
            });
        }
    }

    private IEnumerator ColliderCooldown(CapsuleCollider2D collider)
    {
        yield return new WaitForSeconds(0.2f);
        collider.enabled = false;
    }

    [System.Serializable]
    public class ShadowPoints
    {
        public GameObject[] shadowPoints;
    }
}
