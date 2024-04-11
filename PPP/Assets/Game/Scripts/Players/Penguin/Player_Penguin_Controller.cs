using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Penguin_Controller : MonoBehaviour, IDamageable
{
    // Input fields
    private Player_Penguin _playerPenguinActionsAsset;
    private InputAction _move;

    // Movement fields
    [SerializeField] private float _speed = 7f;

    // Health
    private bool _hasPopsicle = true;
    private bool _isDead;
    private int _health = 1;

    private void Awake()
    {
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

    private void BodyRotate()
    {
        // Change sprites
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
            // Code to drop popscile
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


}
