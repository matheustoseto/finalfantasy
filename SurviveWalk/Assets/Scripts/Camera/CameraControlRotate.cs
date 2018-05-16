using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlRotate : CameraControl {

    private float speedRotation = 0;
    public float inputY_Debug = 0;
    public Vector2 inputRotation = Vector2.zero;

    // Rotate Camera
    [Header("Rotate Camera:")]
    public float speedRotationCamera = 10;
    public float limitRotateMin = -5;
    public float limitRotateMax = 5;

    private float inputMouseScrollWheel = 0;

    // Use this for initialization
    void Start () {
        speedRotation = TargetObject.Player.SpeedRotation;
	}

    protected override void GetInputs()
    {
        base.GetInputs();

        inputMouseScrollWheel += Input.GetAxisRaw("MouseScrollWheel");
        inputRotation += (new Vector2(Input.GetAxisRaw("Vertical"),Input.GetAxisRaw("Horizontal"))).normalized;


        //inputY = Mathf.Clamp(inputY,-50,50);
        inputMouseScrollWheel = Mathf.Clamp(inputMouseScrollWheel, limitRotateMin, limitRotateMax);
    }

    void Update()
    {
        GetInputs();
    }

    void LateUpdate()
    {
        Follow();
        ChangeCameraPosition();
    }

    protected override void Follow()
    {
        Vector3 dir = new Vector3(0, DistancePoint.y, -Mathf.Abs(DistancePoint.z));
        //dir = new Vector3(DistancePoint.x, DistancePoint.y, -Mathf.Abs(DistancePoint.z));

        //Quaternion rotation = Quaternion.Euler(0, inputRotation.y * speedRotation, 0);
        Quaternion rotation = Quaternion.Euler(inputMouseScrollWheel * speedRotationCamera, inputRotation.y * speedRotation, 0);
        transform.position = TargetObject.transform.position + rotation * dir;
        transform.LookAt(TargetObject.transform.position);
    }


}
