using UnityEngine.SceneManagement;

public class TrackMap : IScreenState
{
    public void Display(ScreenManager sm)
    {
        if (!SceneManager.GetActiveScene().name.Equals("Main"))
        {
            // save all parametrs
            SceneManager.LoadScene("Main");
        }
        else
        {
            sm.GUIM.menuP.SetActive(false);
            sm.GUIM.garageP.SetActive(false);
        }

        sm.GUIM.trackMapP.SetActive(true);
    }
}