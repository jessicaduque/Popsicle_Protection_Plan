using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Singleton;
public class Player_Penguin_Controller : Singleton<Player_Penguin_Controller>, IDamageable
{
    // Input fields
    private Player_Penguin _playerPenguinActionsAsset;
    private InputAction _move;

    // Movement fields
    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 7f;

    // Health
    private bool _isDead;
    private int _health = 1;

    // Popsicle
    private bool _hasPopsicle = true;
    [SerializeField] private GameObject _popsicle;

    private new void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPenguinActionsAsset = new Player_Penguin();
    }

    private void OnEnable()
    {
        EnableInputs();
    }

    private void OnDisable()
    {
        DisableInputs();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region Movement

    private void Movement()
    {
        this.transform.position += Vector3.Normalize(new Vector3(_move.ReadValue<Vector2>().x, _move.ReadValue<Vector2>().y)) * _speed * Time.deltaTime;
        BodyRotate();
    }

    #endregion

    #region Sprites

    private void BodyRotate()
    {
        // Change sprites
    }

    private void ChangeSpritePopsicle(bool state)
    {

    }

    #endregion

    #region Input

    public void EnableInputs()
    {
        _playerPenguinActionsAsset.Player.Power.started += DoPowerControl;

        _move = _playerPenguinActionsAsset.Player.Move;
        _playerPenguinActionsAsset.Player.Enable();
    }

    public void DisableInputs()
    {
        _playerPenguinActionsAsset.Player.Power.started -= DoPowerControl;

        _playerPenguinActionsAsset.Player.Disable();
    }

    #endregion

    #region Powers

    private void DoPowerControl(InputAction.CallbackContext obj)
    {
    }

    #endregion

    #region Health

    public void ModifyHealth(int value)
    {
        if (_isDead)
        {
            return;
        }

        if (_hasPopsicle)
        {
            SetHasPopsicle(false);
            _popsicle.SetActive(true);
            return;
        }

        _health += value;

        if(_health <= 0)
        {
            // Code for penguin death
            _isDead = true;
        }
    }

    #endregion

    #region Set

    public void SetHasPopsicle(bool state)
    {
        _hasPopsicle = state;
        _rb.velocity = Vector2.zero;
        ChangeSpritePopsicle(state);
    }

    public void SetVelocity()
    {
        
    }

    #endregion

    #region Get

    public int GetVelocityX()
    {
        return (_move.ReadValue<Vector2>().x > 0 ? -1 : 1);
    }

    #endregion
}
