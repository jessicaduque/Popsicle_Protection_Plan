using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class GameOverController : Singleton<GameOverController>
{
    [SerializeField] private GameObject _penguinWinnerPanel;
    [SerializeField] private GameObject _polarBearWinnerPanel;

    private GameObject _winnerPanel;

    private Button b_replay;
    private Button b_exit;

    private void Start()
    {
        GameOverPanelControl(LevelController.I.GetPenguinIsWinner());
    }

    private void GameOverPanelControl(bool winnerIsPenguin)
    {
        _winnerPanel = (winnerIsPenguin ? _penguinWinnerPanel : _polarBearWinnerPanel);
        ButtonExtra[] buttons = _winnerPanel.GetComponentsInChildren<ButtonExtra>();
        for(int i=0; i< buttons.Length; i++)
        {
            if(buttons[i].nameTag == "replay")
            {
                b_replay = buttons[i].GetComponent<Button>();
            }
            else if(buttons[i].nameTag == "exit")
            {
                b_exit = buttons[i].GetComponent<Button>();
            }
        }
        StartCoroutine(WaitForPanelReady());
        Helpers.FadeInPanel(_winnerPanel);
    }

    private IEnumerator WaitForPanelReady()
    {
        CanvasGroup panelCG = _winnerPanel.GetComponent<CanvasGroup>();
        panelCG.alpha = 0;
        b_replay.enabled = false;
        b_exit.enabled = false;
        while (panelCG.alpha != 1)
        {
            yield return null;
        }
        b_replay.onClick.AddListener(() => {
            AudioManager.I.FadeOutMusic("mainmusic");
            BlackScreenCircleEffectController.I.CircleScreenClose("Main");
            });

        b_exit.onClick.AddListener(() => {
            AudioManager.I.PlayCrossFade("menumusic");
            BlackScreenCircleEffectController.I.CircleScreenClose("MainMenu");
        });
        b_replay.enabled = true;
        b_exit.enabled = true;
    }
}
