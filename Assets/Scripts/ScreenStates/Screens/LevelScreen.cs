using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    private readonly LevelManager levelManager;
    private ScreenManager sm;
    private UILevel ui;

    [SerializeField]
    private string levelName;

    private GameObject playerSpawner;

    private void Awake()
    {
        ui = FindObjectOfType<UILevel>();
    }

    private void Start()
    {
        sm = new ScreenManager(ui);
        new InputHandler(sm);

        sm.SetState(new Game());

        // создание игрока в позиции playerSpawner
    }

    public LevelManager GetLevelManager()
    {
        return levelManager;
    }
}
