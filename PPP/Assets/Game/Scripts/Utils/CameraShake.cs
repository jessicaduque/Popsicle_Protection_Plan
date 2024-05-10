using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float _duration = 1f;
    [SerializeField] private AnimationCurve _curve;

    private void OnEnable()
    {
        Player_Penguin_Controller.I.HealthAffectedEvent += DoCamerShake;
    }

    private void OnDisable()
    {
        Player_Penguin_Controller.I.HealthAffectedEvent -= DoCamerShake;
    }

    private void DoCamerShake()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _duration);
            Vector3 Shake = startPosition + Random.insideUnitSphere * strength;
            Shake = new Vector3(Shake.x, Shake.y, startPosition.z);
            transform.position = Shake;
            yield return null;
        }

        transform.position = startPosition;
    }

}
