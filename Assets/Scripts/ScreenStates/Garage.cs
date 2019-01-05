using UnityEngine;

public class Garage : IScreenState
{
    public void Display(ScreenManager sm)
    {
        sm.GUIM.trackMapP.SetActive(false);
        sm.GUIM.garageP.SetActive(true);
    }
}