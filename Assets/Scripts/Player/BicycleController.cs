using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleController : MonoBehaviour
{
    private LevelScreen levelScreen;

    private Controller controller;
    private Engine engine;

    [SerializeField]
    private Rigidbody2D body;
    //private Transform transform;
    [SerializeField]
    private Rigidbody2D axisFront = null, axisBack = null;
    [SerializeField]
    private bool impulse;
    [SerializeField, Range(0f, 500f)]
    private float wheelForce;

    [Space, SerializeField]
    private float maxSpeed;
    private float currentSpeed;

    [SerializeField, Range(0f, 200f)]
    private float motorForce; // ускорение

    [Space, Header("Wheels")]
    [SerializeField]
    GameObject wheelFront = null;
    [SerializeField]
    GameObject wheelBack = null;

    private Wheel wFront, wBack;
    private Carpet carpet;

    private void Awake()
    {
        levelScreen = FindObjectOfType<LevelScreen>();

        controller = new Controller(1.85f);

        wFront = wheelFront.transform.GetComponent<Wheel>();
        wBack = wheelBack.transform.GetComponent<Wheel>();

        carpet = FindObjectOfType<Carpet>();
    }

    private void Start()
    {
        engine = new Engine(transform, body, axisFront, axisBack, impulse, wheelForce, maxSpeed, motorForce);
    }

    private void FixedUpdate()
    {
        if (!carpet.IsCrash) engine.Move(wFront, wBack, controller);
        else levelScreen.SM.ShowDialog(levelScreen.SM.GUIL.gameOverP);
    }
}