using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // Input fields
    private Player_UI _playerUIActionsAsset;

    [SerializeField] private GameObject _pausePanel;

    private LevelController _levelController => LevelController.I;

    private void Awake()
    {
        _playerUIActionsAsset = new Player_UI();
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

    #region Input

    public void EnableInputs()
    {
        _playerUIActionsAsset.UI.PauseGame.started += DoPauseControl;

        _playerUIActionsAsset.UI.Enable();
    }

    public void DisableInputs()
    {
        _playerUIActionsAsset.UI.PauseGame.started -= DoPauseControl;

        _playerUIActionsAsset.UI.Disable();
    }

    #endregion

    #region Pause Control

    private void DoPauseControl(InputAction.CallbackContext obj)
    {
        Time.timeScale = 0;

        Helpers.FadeInPanel(_pausePanel);
    }

    #endregion
}
