using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeMove { Original, RotateY, Turn }
public class IcarusController : MonoBehaviour {

    public Transform body = null;

    public GameObject seta;
    public float runSpeed = 10;
    public float speedRotation = 250;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;
    public TypeMove typeMove = TypeMove.RotateY;

    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private float velocityY;

    public Vector2 input = Vector2.zero;

    private CharacterController controller;

    public  float SpeedRotation { get { return speedRotation; } }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        seta.SetActive(true);
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        Move(input);
    }

    void Move(Vector2 dir)
    {
        switch (typeMove)
        {
            case TypeMove.Original: MoveOriginal(dir); return;
            case TypeMove.RotateY: MoveRotateY(dir); return;
            case TypeMove.Turn: MoveTurn(dir); return;
            default:
                MoveOriginal(dir); return;
        }
    }


    void MoveRotateY(Vector2 dir)
    {
        Vector2 inputDir = dir.normalized;

        float targetSpeed = runSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        // Efetuar movimentação e rotação em Y
        transform.Rotate(0, inputDir.x * speedRotation, 0);

        // Efetuar delocamento no cenário
        Vector3 speed = Vector3.zero;
        speed.x = 0; // inputDir.x;
        speed.z = inputDir.y;
        //speed.Normalize();

        ////Correr
        //float inputRun = 0; // Parâmetro
        //if (inputRun > 0)
        //    speedActual = speedRun;
        //else
        //    speedActual = speedWalk;

        Vector3 move = transform.TransformDirection(speed * currentSpeed * Time.deltaTime);

        controller.Move(move);


    }

    
    void MoveTurn(Vector2 dir)
    {
        Vector2 inputDir = dir.normalized;

        /*if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        */
        float targetSpeed = runSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        //Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        //controller.Move(velocity * Time.deltaTime);


        ///////////////////////////////////////////////////////////////////////////////

        // Body Rotate //
        BodyRotateKeyboard(inputDir.y, inputDir.x);

        // Efetuar delocamento no cenário
        Vector3 speed = Vector3.zero;
        speed.x = inputDir.x;
        speed.z = inputDir.y;
        //speed.Normalize();

        ////Correr
        //float inputRun = 0; // Parâmetro
        //if (inputRun > 0)
        //    speedActual = speedRun;
        //else
        //    speedActual = speedWalk;

        Vector3 move = transform.TransformDirection(speed * currentSpeed * Time.deltaTime);
        controller.Move(move);
    }

    void MoveOriginal(Vector2 dir)
    {
        Vector2 inputDir = dir.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        float targetSpeed = runSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
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

}
