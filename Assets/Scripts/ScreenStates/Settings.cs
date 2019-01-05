using UnityEngine.SceneManagement;

public class Settings : IScreenState
{
    public void Display(ScreenManager sm)
    {
        if (!SceneManager.GetActiveScene().name.Equals("Main"))
        {
            sm.GUIL.pauseP.SetActive(false);
            sm.GUIL.settingsP.SetActive(true);
        }
        else
        {
            sm.GUIM.menuP.SetActive(false);
            sm.GUIM.settingsP.SetActive(true);
        }
    }
}