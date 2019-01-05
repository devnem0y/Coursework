using UnityEngine;

public class Menu : IScreenState
{
    public void Display(ScreenManager sm)
    {
        sm.GUIM.trackMapP.SetActive(false);
        sm.GUIM.menuP.SetActive(true);
    }
}