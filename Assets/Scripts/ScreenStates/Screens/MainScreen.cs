using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    private ScreenManager sm;
    private UIMenu ui;

    private void Awake()
    {
        ui = FindObjectOfType<UIMenu>();
    }

    private void Start()
    {
        sm = new ScreenManager(ui);
        new InputHandler(sm);

        sm.SetState(new Menu());
    }
}