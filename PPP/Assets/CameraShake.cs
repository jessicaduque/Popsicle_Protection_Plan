using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float _duration = 1f;
    [SerializeField] private AnimationCurve _curve;

    private void OnEnable()
    {
        Player_Penguin_Controller.I.HealthAffectedEvent += () => StartCoroutine(DoCameraShake());
    }

    private void OnDisable()
    {
        Player_Penguin_Controller.I.HealthAffectedEvent -= () => StartCoroutine(DoCameraShake());
    }

    IEnumerator DoCameraShake()
    {
        StopAllCoroutines();

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

}
