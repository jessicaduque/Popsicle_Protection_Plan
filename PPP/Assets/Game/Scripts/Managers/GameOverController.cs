using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class GameOverController : Singleton<GameOverController>
{
    [SerializeField] private GameObject _penguinWinnerPanel;
    [SerializeField] private GameObject _polarBearWinnerPanel;

    private GameObject _winnerPanel;

    [SerializeField] private Button b_restart;
    [SerializeField] private Button b_mainMenu;

    private BlackScreenController _blackScreenController => BlackScreenController.I;
    private LevelController _levelController => LevelController.I;
    private void OnEnable()
    {
        _levelController.timeUp += CheckWinner;
    }
    private void OnDisable()
    {
        _levelController.timeUp -= CheckWinner;
    }

    private IEnumerator WaitForPanelReady()
    {
        CanvasGroup panelCG = _winnerPanel.GetComponent<CanvasGroup>();
        while (panelCG.alpha != 1)
        {
            yield return null;
        }

        b_restart.onClick.AddListener(() => _blackScreenController.FadeOutScene("Main"));
        b_mainMenu.onClick.AddListener(() => _blackScreenController.FadeOutScene("MainMenu"));
    }

    private void CheckWinner()
    {
        if (Player_Polar_Bear_Controller.I._isDead)
        {
            GameOverPanelControl(false);
            return;
        }

        if (Player_Penguin_Controller.I._hasPopsicle)
        {
            GameOverPanelControl(true);
        }
        else
        {
            GameOverPanelControl(false);
        }
    }

    private void GameOverPanelControl(bool winnerIsPenguin)
    {
        _winnerPanel = (winnerIsPenguin ? _penguinWinnerPanel : _polarBearWinnerPanel);
        Helpers.FadeInPanel(_winnerPanel);
    }
}
