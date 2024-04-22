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

    private Button b_restart;
    private Button b_mainMenu;

    private BlackScreenController _blackScreenController => BlackScreenController.I;
    private LevelController _levelController => LevelController.I;

    private new void Awake()
    {
        
    }
    private void OnEnable()
    {
        _levelController.timeUp += CheckWinner;
    }
    private void OnDisable()
    {
        _levelController.timeUp -= CheckWinner;
        b_mainMenu.onClick.RemoveAllListeners();
        b_restart.onClick.RemoveAllListeners();
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
        ButtonExtra[] buttons = _winnerPanel.GetComponentsInChildren<ButtonExtra>();
        for(int i=0; i< buttons.Length; i++)
        {
            if(buttons[i].nameTag == "restart")
            {
                b_restart = buttons[i].GetComponent<Button>();
            }
            else if(buttons[i].nameTag == "menu")
            {
                b_mainMenu = buttons[i].GetComponent<Button>();
            }
        }
        StartCoroutine(WaitForPanelReady());
        Helpers.FadeInPanel(_winnerPanel);
    }

    private IEnumerator WaitForPanelReady()
    {
        CanvasGroup panelCG = _winnerPanel.GetComponent<CanvasGroup>();
        panelCG.alpha = 0;
        b_restart.enabled = false;
        b_mainMenu.enabled = false;
        while (panelCG.alpha != 1)
        {
            yield return null;
        }
        b_restart.onClick.AddListener(() => _blackScreenController.FadeOutScene("Main"));
        b_mainMenu.onClick.AddListener(() => _blackScreenController.FadeOutScene("MainMenu"));
        b_restart.enabled = true;
        b_mainMenu.enabled = true;
    }
}
