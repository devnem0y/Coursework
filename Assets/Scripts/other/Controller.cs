using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    public Vector2 StartPos{ get; set; }
    public Vector2 EndPos{ get; set; }
    public float Offset { get; set; }

    public Controller(float offset)
    {
        Offset = offset;
    }
}