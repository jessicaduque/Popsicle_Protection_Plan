using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Power_Snowberus : Power
{
    [SerializeField] private ShadowPoints[] snowFallShadows;
    [SerializeField] private Pool bigSnowballPoolItem;
    [SerializeField] private float bigSnowballSpawnY;

    private float _timeBetweenSnowballs, _timeForShadowToAppear;
    private float _timeForSnowballFall = 0.4f;

    private AudioManager _audioManager => AudioManager.I;
    private PoolManager _poolManager => PoolManager.I;

    private void Start()
    {
        UsageTime = LevelController.I._levelPolarBearBlessingSO.power_useTime;

        _timeBetweenSnowballs = (UsageTime / 6) - (_timeForSnowballFall / 2);
        _timeForShadowToAppear = (UsageTime / 2) - (_timeForSnowballFall / 2); 
    }

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
        int shadowsArray = Random.Range(0, snowFallShadows.Length);
        Vector2 snowBallSpawnPoint;
        
        
        for (int i = 0; i < 3; i++)
        {
            snowBallSpawnPoint.x = snowFallShadows[shadowsArray].shadowPoints[i].transform.position.x;
            snowBallSpawnPoint.y = bigSnowballSpawnY + snowFallShadows[shadowsArray].shadowPoints[i].transform.position.y;
            SpriteRenderer shadowSpriteRenderer = snowFallShadows[shadowsArray].shadowPoints[i].GetComponent<SpriteRenderer>();
            Transform shadowTransform = shadowSpriteRenderer.transform;
            shadowTransform.localScale = Vector3.zero;
            
            shadowSpriteRenderer.DOFade(1, _timeForShadowToAppear).SetDelay(i * _timeBetweenSnowballs);
            shadowTransform.DOScale(Vector3.one * 4, _timeForShadowToAppear).SetDelay(i * _timeBetweenSnowballs);
            GameObject bigSnowball = _poolManager.GetObject(bigSnowballPoolItem.tagPool, snowBallSpawnPoint, Quaternion.identity);
            bigSnowball.transform.DOMoveY(snowFallShadows[shadowsArray].shadowPoints[i].transform.position.y, _timeForSnowballFall).SetDelay((i * _timeBetweenSnowballs) + (_timeForShadowToAppear - _timeForSnowballFall)).OnComplete(() =>
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
