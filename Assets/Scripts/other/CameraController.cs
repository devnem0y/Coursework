using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Target { get; set; }

    private void Update()
    {
        transform.position = new Vector3(Target.position.x + 0.7f, Target.position.y + 2.5f, -1f);
    }
}