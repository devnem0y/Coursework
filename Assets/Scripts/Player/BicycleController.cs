using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleController : MonoBehaviour
{
    private Controller controller;

    [SerializeField]
    private Engine engine;

    [Space, Header("Wheels")]
    [SerializeField]
    GameObject wheelFront = null;
    [SerializeField]
    GameObject wheelBack = null;

    private Wheel wFront, wBack;

    private bool isCrash = false;

    private void Awake()
    {
        controller = new Controller(1.85f);

        wFront = wheelFront.transform.GetComponent<Wheel>();
        wBack = wheelBack.transform.GetComponent<Wheel>();
    }

    private void Start()
    {
        engine = new Engine(transform);
    }

    private void FixedUpdate()
    {
        if (!isCrash) engine.Move(wFront, wBack, controller);
    }
}