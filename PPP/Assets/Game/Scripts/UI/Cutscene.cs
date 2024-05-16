using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public void CutsceneEnd()
    {
        MainMenuUIManager.I.EndCutscene();
    }
}
