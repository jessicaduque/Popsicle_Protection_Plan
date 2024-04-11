using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Polar_Bear_Controller : MonoBehaviour, IDamageable
{
    // Input fields
    private Player_Polar_Bear _playerPolarBearActionsAsset;
    private InputAction _move;

    // Movement fields
    [SerializeField] private float _speed = 8f;

    // Health
    private bool _isDead;
    private int _health = 1;

    // Attack
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Pool _attackPoolItem;
    private bool _canAttack = true;

    private PoolManager _poolManager => PoolManager.I;

    private void Awake()
    {
        _playerPolarBearActionsAsset = new Player_Polar_Bear();
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

        _move = _playerPolarBearActionsAsset.Player.Move;
        _playerPolarBearActionsAsset.Player.Enable();
    }

    public void DisableInputs()
    {
        _playerPolarBearActionsAsset.Player.Power.started -= DoPowerControl;
        _playerPolarBearActionsAsset.Player.Attack.started -= DoAttackControl;

        _playerPolarBearActionsAsset.Player.Disable();
    }

    #endregion

    #region Powers

    private void DoPowerControl(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
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

        _poolManager.GetObject(_attackPoolItem.tagPool, _attackPoint.position, _attackPoint.rotation);
        StartCoroutine(AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.2f);

        _canAttack = true;
    }

    #endregion
}
