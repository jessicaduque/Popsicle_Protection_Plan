using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Singleton;

public class GameController : DontDestroySingleton<GameController>
{
    BlackScreenController _blackScreenController => BlackScreenController.I;
    
    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstPlay") || PlayerPrefs.GetInt("FirstPlay") == 0)
        {
            _blackScreenController.FadeInSceneStart();
            MainMenuUIManager.I.StartCoroutine(MainMenuUIManager.I.StartCutscene());
            PlayerPrefs.SetInt("FirstPlay", 1);
        }
        else
        {
            MainMenuUIManager.I.DisableCutscene();
        }
        
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            MainMenuUIManager.I.DisableCutscene();
        }
    }
}
