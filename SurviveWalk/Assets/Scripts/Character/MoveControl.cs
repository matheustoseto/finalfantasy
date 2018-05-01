using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CharacterController))]
public class MoveControl : MonoBehaviour
{
    public Transform body = null;
    public GameObject arrow = null;
    public float speedWalk = 10;
    public float speedRun = 20;
    public float gravity = 20.0F;
    public bool isBodyRotateKeyboard = true;

    private float speedActual = 0;
    private Vector3 speed = Vector2.zero;
    private CharacterController charControl = null;

    public float SpeedWalk { get { return speedWalk; } }
    public float SpeedRun { get { return speedRun; } }

    public void Awake()
    {
        if (arrow != null)
            arrow.SetActive(true);
    }

    // Use this for initialization
    void Start()
    {
        charControl = GetComponent<CharacterController>();
        Stop();
    }

   

    // Update is called once per frame
    void Update()
    {
        //RotateWithMouse(transform);
    }

    public void Stop()
    {
        speedActual = 0;
        speed = Vector3.zero;
    }

    public void Move(float inputForward, float inputTurn)
    {

        if(isBodyRotateKeyboard)
            BodyRotateKeyboard(inputForward, inputTurn);
        //BodyDirectionMouse(inputForward, inputTurn);

        // Efetuar delocamento no cenário
        speed.z = inputForward;
        speed.x = inputTurn;
        speed.Normalize();

        ////Correr
        float inputRun = 0; // Parâmetro
        if (inputRun > 0)
            speedActual = speedRun;
        else
            speedActual = speedWalk;

        Vector3 move = transform.TransformDirection(speed * speedActual * Time.deltaTime);

        move.y -= gravity * Time.deltaTime;

        charControl.Move(move);
    }

    private void BodyRotateKeyboard(float inputForward, float inputTurn)
    {
        // Efetuar  rotação em Y
        /*if (inputForward == 0 && inputTurn == 0) body.transform.eulerAngles = new Vector3(0, 180, 0); // Center
        else*/
        if (inputForward > 0 && inputTurn == 0) body.transform.eulerAngles = new Vector3(0, 0, 0); // Up
        else if (inputForward > 0 && inputTurn > 0) body.transform.eulerAngles = new Vector3(0, 45, 0); // UpRight
        else if (inputForward == 0 && inputTurn > 0) body.transform.eulerAngles = new Vector3(0, 90, 0); // Right
        else if (inputForward < 0 && inputTurn > 0) body.transform.eulerAngles = new Vector3(0, 135, 0); // DownRight
        else if (inputForward < 0 && inputTurn == 0) body.transform.eulerAngles = new Vector3(0, 180, 0); // Down
        else if (inputForward < 0 && inputTurn < 0) body.transform.eulerAngles = new Vector3(0, 225, 0); // DownLeft
        else if (inputForward == 0 && inputTurn < 0) body.transform.eulerAngles = new Vector3(0, 270, 0); // Left
        else if (inputForward > 0 && inputTurn < 0) body.transform.eulerAngles = new Vector3(0, 315, 0); // UpLeft
    }

    /*
    private void RotateWithMouse(Transform goTransform)
    {
        //Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - goTransform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //goTransform.rotation = Quaternion.Slerp(goTransform.rotation, rotation, 5 * Time.deltaTime);

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }*/
}
