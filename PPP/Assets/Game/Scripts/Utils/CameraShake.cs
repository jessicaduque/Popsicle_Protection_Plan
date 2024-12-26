using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private float _duration = 1f;
    [SerializeField] private AnimationCurve _curve;
    
    private Player_Penguin_Controller _playerPenguinController => Player_Penguin_Controller.I;


    private void Start()
    {
        _playerPenguinController.HealthAffectedEvent += DoCameraShake;
    }

    private void DoCameraShake()
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
            elapsedTime += Time.unscaledDeltaTime;
            float strength = _curve.Evaluate(elapsedTime / _duration);
            Vector3 Shake = startPosition + Random.insideUnitSphere * strength;
            Shake = new Vector3(Shake.x, Shake.y, startPosition.z);
            transform.position = Shake;
            yield return null;
        }

        transform.position = startPosition;
    }

}
