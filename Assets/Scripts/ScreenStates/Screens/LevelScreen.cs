using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    private readonly LevelManager levelManager;
    public ScreenManager SM { get; private set; }
    private UILevel ui;

    [SerializeField]
    private string levelName;
    [SerializeField]
    private GameObject playerSpawner;
    private CameraController cameraController;

    private void Awake()
    {
        ui = FindObjectOfType<UILevel>();
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        SM = new ScreenManager(ui);
        new InputHandler(SM);

        SM.SetState(new Game());

        // создание игрока в позиции playerSpawner
        Vector3 position = playerSpawner.transform.position;
        GameObject player = Instantiate(Resources.Load<GameObject>("bike"), position, Quaternion.identity);

        cameraController.Target = player.transform;
    }

    public LevelManager GetLevelManager()
    {
        return levelManager;
    }
}
