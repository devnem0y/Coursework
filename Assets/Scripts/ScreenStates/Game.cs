using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : IScreenState
{
    public void Display(ScreenManager sm)
    {
        Time.timeScale = 1;
        sm.GUIL.pauseP.SetActive(false);
        sm.GUIL.gameOverP.SetActive(false);

        sm.GUIL.gameP.SetActive(true);
    }
}
