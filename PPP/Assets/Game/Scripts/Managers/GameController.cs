using UnityEngine.SceneManagement;
using Utils.Singleton;

public class GameController : DontDestroySingleton<GameController>
{
    BlackScreenController _blackScreenController => BlackScreenController.I;
    
    private void Start()
    {
        _blackScreenController.FadeInSceneStart();
        MainMenuUIManager.I.StartCoroutine(MainMenuUIManager.I.StartCutscene());
        
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
