using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Popsicle : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameObject _player;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _impulseForce = 4;
    private Player_Penguin_Controller _playerPenguinController => Player_Penguin_Controller.I;
    AudioManager _audioManager => AudioManager.I;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = Player_Penguin_Controller.I.gameObject;
        _collider.enabled = false;
    }

    private void OnEnable()
    {
        transform.position = _player.transform.position;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _rb.gravityScale = 1;
        _rb.AddForce(new Vector2(_playerPenguinController.GetVelocityX() * _impulseForce, 2), ForceMode2D.Impulse);
        _rb.AddTorque(_torqueForce, ForceMode2D.Impulse);
        StartCoroutine(StopMovement());
    }

    private void OnDisable()
    {
        _collider.enabled = false;
        DOTween.KillAll();
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _audioManager.PlaySfx("popsicleget");
            _playerPenguinController.SetHasPopsicle(true);
            this.gameObject.SetActive(false);
        }
    }

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovementBorder"))
        {
            _collider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collider.enabled = true;
        }
    }
    #endregion

    private IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(1);
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        DOTween.To(() => _rb.angularVelocity, x => _rb.angularVelocity = x, 0, 2);
        DOTween.To(() => _rb.velocity.x, x => _rb.velocity = new Vector2(x, 0), 0, 2);
    }
}
