using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Singleton;
public class Player_Penguin_Controller : Singleton<Player_Penguin_Controller>, IDamageable
{
    // Input fields
    private Player_Penguin _playerPenguinActionsAsset;
    private Animator _animator;
    private InputAction _move;

    // Movement fields
    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 7f;

    // Health
    public bool _isDead { get; private set; }
    private int _health = 1;
    public event Action HealthAffectedEvent;

    // Popsicle
    public bool _hasPopsicle { get; private set; } = true;
    [SerializeField] private GameObject _popsicle;

    // Power
    [SerializeField] private Power_SO _powerSO;
    [SerializeField] private Power _powerScript;
    private LevelController _levelController;

    private new void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        SetHasPopsicle(true);
        _playerPenguinActionsAsset = new Player_Penguin();
        _move = _playerPenguinActionsAsset.Player.Move;
    }

    private void Start()
    {
        _levelController = LevelController.I;
        _levelController.blessingsRandomizedEvent += () => SetPower(_levelController._levelPenguinBlessingSO, _levelController._levelPenguinBlessing);
    }

    private void OnEnable()
    {
        _levelController.beginLevelEvent += EnableInputs;

        _levelController.timeUpEvent += DisableInputs;
        _levelController.pauseEvent += DisableInputs;
    }

    private void OnDisable()
    {
        _levelController.beginLevelEvent -= EnableInputs;

        _levelController.timeUpEvent -= DisableInputs;
        _levelController.pauseEvent -= DisableInputs;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region Movement

    private void Movement()
    {
        this.transform.position += Vector3.Normalize(new Vector3(_move.ReadValue<Vector2>().x, _move.ReadValue<Vector2>().y)) * _speed * Time.deltaTime;
        MovementAnimationControl();
    }

    #endregion

    #region Animation

    private void MovementAnimationControl()
    {
        _animator.SetFloat("Walk_X", _move.ReadValue<Vector2>().x);
        _animator.SetFloat("Walk_Y", _move.ReadValue<Vector2>().y);
    }

    private void AnimationPopsicleControl(bool state)
    {
        _animator.SetLayerWeight(1, (state ? 1 : 0));
    }

    #endregion

    #region Input

    public void EnableInputs()
    {
        _playerPenguinActionsAsset.Player.Power.started += DoPowerControl;

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
        Debug.Log("do power control");
        _powerScript.UsePower();
    }

    #endregion

    #region Health

    public void ModifyHealth(int value)
    {
        if (_isDead)
        {
            return;
        }

        if(value < 0)
            HealthAffectedEvent?.Invoke();

        if (_hasPopsicle)
        {
            SetHasPopsicle(false);
            _popsicle.SetActive(true);
            return;
        }

        _health += value;

        if(_health <= 0)
        {
            _isDead = true;
            TimeCountManager.I.SetTimeUp();
        }
    }

    #endregion

    #region Set

    public void SetHasPopsicle(bool state)
    {
        _hasPopsicle = state;
        _rb.velocity = Vector2.zero;
        AnimationPopsicleControl(state);
    }

    private void SetPower(Power_SO powerSO, Power power)
    {
        _powerSO = powerSO;
        _powerScript = power;
    }

    #endregion

    #region Get
    public Power GetPower()
    {
        return _powerScript;
    }
    public int GetVelocityX()
    {
        return (_move.ReadValue<Vector2>().x > 0 ? -1 : 1);
    }

    #endregion
}
