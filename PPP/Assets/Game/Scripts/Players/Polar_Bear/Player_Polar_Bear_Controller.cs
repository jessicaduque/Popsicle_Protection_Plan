using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Singleton;
public class Player_Polar_Bear_Controller : Singleton<Player_Polar_Bear_Controller>, IDamageable
{
    // Input fields
    private Player_Polar_Bear _playerPolarBearActionsAsset;
    private InputAction _move;

    // Movement fields
    [SerializeField] private float _speed = 8f;

    // Health
    public bool _isDead { get; private set; }
    private int _health = 1;

    // Attack
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Pool _attackPoolItem;
    [SerializeField] private float _attackCooldownTime;
    private bool _canAttack = true;

    // Power
    [SerializeField] private Power_SO _powerSO;
    [SerializeField] private Power _powerScript;

    private PoolManager _poolManager => PoolManager.I;
    private LevelController _levelController => LevelController.I;

    private new void Awake()
    {
        _playerPolarBearActionsAsset = new Player_Polar_Bear();

        _move = _playerPolarBearActionsAsset.Player.Move;
    }

    private void OnEnable()
    {
        _levelController.beginLevel += EnableInputs;
        _levelController.timeUp += DisableInputs;
    }

    private void OnDisable()
    {
        _levelController.beginLevel -= EnableInputs;
        _levelController.timeUp -= DisableInputs;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    #region Movement

    private void Movement()
    {
        float speedX = _move.ReadValue<Vector2>().x;
        this.transform.position += new Vector3(speedX, 0) * _speed * Time.deltaTime;
        BodyRotate(speedX);
    }

    private void BodyRotate(float speedX)
    {
        if (speedX > 0f)
            this.transform.localScale = new Vector3(1, 1, 1);
        else
            this.transform.localScale = new Vector3(-1, 1, 1);
    }

    #endregion

    #region Input

    public void EnableInputs()
    {
        _playerPolarBearActionsAsset.Player.Power.started += DoPowerControl;
        _playerPolarBearActionsAsset.Player.Attack.started += DoAttackControl;

        _playerPolarBearActionsAsset.Player.Enable();
    }

    public void DisableInputs()
    {
        _playerPolarBearActionsAsset.Player.Power.started -= DoPowerControl;
        _playerPolarBearActionsAsset.Player.Attack.started -= DoAttackControl;

        _playerPolarBearActionsAsset.Player.Disable();
    }

    #endregion

    #region Power

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

        _health += value;

        if (_health <= 0)
        {
            // Code for polar bear death
            _isDead = true;
        }
    }

    #endregion

    #region Attack 

    private void DoAttackControl(InputAction.CallbackContext obj)
    {
        if (!_canAttack)
        {
            return;
        }

        _poolManager.GetObject(_attackPoolItem.tagPool, _attackPoint.position, Quaternion.identity);
        StartCoroutine(AttackCoolDown());
        _canAttack = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(_attackCooldownTime);

        _canAttack = true;
    }

    #endregion


    #region Set
    public void SetPower(Power_SO power)
    {
        _powerSO = power;
        GameObject powerController = Instantiate(power.power_controllerPrefab, Vector2.zero, Quaternion.identity);
        _powerScript = powerController.GetComponent<Power>();
    }

    #endregion
}
