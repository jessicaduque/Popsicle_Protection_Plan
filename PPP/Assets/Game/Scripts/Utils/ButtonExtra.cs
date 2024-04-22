using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExtra : MonoBehaviour
{
    [field: SerializeField] public string nameTag { get; private set; }
    private Button _thisButton;
    private bool _canClick = true;
    AudioManager _audioManager => AudioManager.I;

    private void Awake()
    {
        _thisButton = GetComponent<Button>();
        _thisButton.onClick.AddListener(MakeSound);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void MakeSound()
    {
        if (_canClick)
        {
            //_audioManager.PlaySfx("ButtonClick");
            _thisButton.enabled = false;
            StartCoroutine(Reset());
            _canClick = false;
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.1f);
        _canClick = true;
    }

}
