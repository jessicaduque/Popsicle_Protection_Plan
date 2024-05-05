using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject _menuPanel;

    [Header("Play")]
    [SerializeField] Button b_play;

    [Header("Settings")]
    [SerializeField] Button b_settings;
    [SerializeField] GameObject _settingsPanel;

    [Header("Credits")]
    [SerializeField] Button b_credits;
    [SerializeField] GameObject _creditsPanel;

    [Header("How To Play")]
    [SerializeField] Button b_howToPlay;
    [SerializeField] GameObject _howToPlayPanel;

    BlackScreenController _blackScreenController => BlackScreenController.I;

    private void OnEnable()
    {
        StartCoroutine(WaitForPanelReady());
        ButtonThresholdSetup();
        AddButtonListeners();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    #region Play

    private void PlayButtonControl()
    {
        _blackScreenController.FadeOutScene("Main");
    }

    #endregion

    #region Settings

    private void SettingsButtonControl()
    {
        _blackScreenController.FadePanel(_settingsPanel, true);
    }

    #endregion

    #region Credits

    private void CreditsButtonControl()
    {
        _blackScreenController.FadePanel(_creditsPanel, true);
    }

    #endregion

    #region How To Play

    private void HowToPlayButtonControl()
    {
        _blackScreenController.FadePanel(_howToPlayPanel, true);
    }

    #endregion

    private void ButtonThresholdSetup()
    {
        float threshold = 0.4f;

        b_play.GetComponent<Image>().alphaHitTestMinimumThreshold = threshold;
        b_settings.GetComponent<Image>().alphaHitTestMinimumThreshold = threshold;
        b_credits.GetComponent<Image>().alphaHitTestMinimumThreshold = threshold;
        b_howToPlay.GetComponent<Image>().alphaHitTestMinimumThreshold = threshold;
    }

    private void AddButtonListeners()
    {
        b_play.onClick.AddListener(PlayButtonControl);
        b_settings.onClick.AddListener(SettingsButtonControl);
        b_credits.onClick.AddListener(CreditsButtonControl);
        b_howToPlay.onClick.AddListener(HowToPlayButtonControl);
    }

    private IEnumerator WaitForPanelReady()
    {
        ButtonEnableControl(false);

        yield return new WaitForSeconds(Helpers.blackFadeTime);

        ButtonEnableControl(true);
    }

    private void ButtonEnableControl(bool state)
    {
        b_play.enabled = state;
        b_settings.enabled = state;
        b_credits.enabled = state;
        b_howToPlay.enabled = state;
    }
}
