using UnityEngine;

public class ScreenManager
{
    private IScreenState screenState;
    public UIMenu GUIM { get; }
    public UILevel GUIL { get; }
    public string CurrentState { get; set; }

    public ScreenManager(UIMenu ui)
    {
        GUIM = ui;
    }

    public ScreenManager(UILevel ui)
    {
        GUIL = ui;
    }

    public void SetState(IScreenState s)
    {
        this.screenState = s;
        screenState.Display(this);
    }

    public void ShowDialog(GameObject panel)
    {
        panel.SetActive(true);
    }
}