using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Power_SnowCerberus : Power
{
    [SerializeField] private ShadowPoints[] _snowFallShadows;
    [SerializeField] private Pool _bigSnowballPoolItem;
    [SerializeField] private float _bigSnowballSpawnY;

    private CanvasGroup[] _snowFallShadowsCanvasGroup = new CanvasGroup[3];
    private float _shadowFadeTime = 4;
    private float _snowballFallTime = 1;

    private PoolManager _poolManager => PoolManager.I;
    public override void UsePower()
    {
        base.UsePower();

        SnowFall();
    }
    
    private void SnowFall()
    {
        int shadowsArray = Random.Range(0, _snowFallShadows.Length);
        Vector2 snowBallSpawnPoint = new Vector2(0, _bigSnowballSpawnY);

        for (int i=0; i < 3; i++)
        {
            snowBallSpawnPoint.x = _snowFallShadows[shadowsArray].shadowPoints[i].transform.position.x;

            _snowFallShadowsCanvasGroup[i] = _snowFallShadows[shadowsArray].shadowPoints[i].GetComponent<CanvasGroup>();

            _snowFallShadowsCanvasGroup[i].DOFade(1, _shadowFadeTime).SetDelay(i * 0.4f);
            GameObject bigSnowball =_poolManager.GetObject(_bigSnowballPoolItem.tagPool, snowBallSpawnPoint, Quaternion.identity);
            bigSnowball.transform.DOMoveY(_snowFallShadows[shadowsArray].shadowPoints[i].transform.position.y, _snowballFallTime).SetDelay(i * 0.4f + (_shadowFadeTime - _snowballFallTime));

        }
    }

    [System.Serializable]
    public class ShadowPoints
    {
        public GameObject[] shadowPoints;
    }
}
