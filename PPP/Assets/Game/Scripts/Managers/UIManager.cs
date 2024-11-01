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

    private LevelController _levelController;
    private AudioManager _audioManager => AudioManager.I;
    private void Awake()
    {
        _playerUIActionsAsset = new Player_UI();
    }

    private void Start()
    {
        _levelController = LevelController.I;
        _levelController.blessingsEvent += () => _blessingPanel.SetActive(true);
        _levelController.countdownEvent += () => { 
            Helpers.FadeOutPanel(_blessingPanel);
            Helpers.FadeInPanel(_countdownPanel);
            _audioManager.FadeInMusic("mainmusic");
        };
        _levelController.beginLevelEvent += () => Helpers.FadeCrossPanel(_countdownPanel, _hudPanel);
        _levelController.timeUpEvent += () =>
        {
            _gameoverPanels.SetActive(true);
            _audioManager.PlaySfx("levelend");
        };

        _levelController.BeginBlessings();
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
        _levelController.Pause();

        Helpers.FadeInPanel(_pausePanel);
    }

    #endregion
}
