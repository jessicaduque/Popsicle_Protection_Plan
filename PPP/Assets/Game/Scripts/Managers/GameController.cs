using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Singleton;

public class GameController : DontDestroySingleton<GameController>
{
    BlackScreenController _blackScreenController => BlackScreenController.I;
    
    private void Start()
    {
        _blackScreenController.FadeInSceneStart();
#if PLATFORM_WEBGL
        MainMenuUIManager.I.StartCoroutine(MainMenuUIManager.I.StartCutscene());
#else
        if (!PlayerPrefs.HasKey("FirstPlay") || PlayerPrefs.GetInt("FirstPlay") == 0)
        {
            MainMenuUIManager.I.StartCoroutine(MainMenuUIManager.I.StartCutscene());
            PlayerPrefs.SetInt("FirstPlay", 1);
        }
        else
        {
            MainMenuUIManager.I.DisableCutscene();
        }
#endif    
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            MainMenuUIManager.I.DisableCutscene();
            _blackScreenController.FadeInSceneStart();
        }
    }
}
