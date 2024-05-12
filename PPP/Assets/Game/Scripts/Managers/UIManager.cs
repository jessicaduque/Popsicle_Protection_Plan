using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // Input fields
    private Player_UI _playerUIActionsAsset;

    [Header("Panels for each game state")]
    [SerializeField] private GameObject _blessingPanel;
    [SerializeField] private GameObject _countdownPanel;
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private GameObject _gameoverPanels;
    [SerializeField] private GameObject _pausePanel;

    private LevelController _levelController => LevelController.I;

    private void Awake()
    {
        _playerUIActionsAsset = new Player_UI();
    }

    private void Start()
    {
        _levelController.blessingsEvent += () => _blessingPanel.SetActive(true);
        _levelController.countdownEvent += () => Helpers.FadeCrossPanel(_blessingPanel, _countdownPanel);
        _levelController.beginLevelEvent += () => { _countdownPanel.SetActive(false); _hudPanel.SetActive(true); };
        _levelController.timeUpEvent += () => _gameoverPanels.SetActive(true);

        _levelController.BeginBlessings();
    }

    private void OnEnable()
    {
        _levelController.beginLevelEvent += EnableInputs;
        _levelController.timeUpEvent += DisableInputs;
    }

    private void OnDisable()
    {
        _levelController.beginLevelEvent -= EnableInputs;
        _levelController.timeUpEvent -= DisableInputs;
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
