using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Singleton;

public class UIManager : Singleton<UIManager>
{
    // Input fields
    private Player_UI _playerUIActionsAsset;

    [Header("Panels for each game state")]
    [SerializeField] private GameObject _blessingPanel;
    [SerializeField] private GameObject _countdownPanel;
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _pausePanel;

    LevelController _levelController => LevelController.I;
    private AudioManager _audioManager => AudioManager.I;
    protected override void Awake()
    {
        base.Awake();
        
        _playerUIActionsAsset = new Player_UI();
    }

    private void Start()
    {
        _levelController.beginLevelEvent += EnableInputs;
        _levelController.pauseEvent += DisableInputs;
        _levelController.blessingsEvent += () => _blessingPanel.SetActive(true);
        _levelController.countdownEvent += () =>
        {
            Helpers.FadeOutPanel(_blessingPanel);
            Helpers.FadeInPanel(_countdownPanel);
            _audioManager.FadeInMusic("mainmusic");
        };
        _levelController.beginLevelEvent += () => Helpers.FadeCrossPanel(_countdownPanel, _hudPanel);
        _levelController.timeUpEvent += () =>
        {
            DisableInputs();
            _audioManager.PlaySfx("levelend");
        };

        _levelController.BeginBlessings();
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

    #region Gameover Control

    public void ActiavateGameOverPanel()
    {
        Helpers.FadeInPanel(_gameOverPanel);
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
