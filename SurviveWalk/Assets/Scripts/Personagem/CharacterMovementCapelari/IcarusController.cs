using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcarusController : MonoBehaviour {

    public GameObject seta;
    public float runSpeed = 10;
    public float turnSmoothTime = 0.2f;
    public float speedSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private float velocityY;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        seta.SetActive(true);
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Move(input);
    }

    void Move(Vector2 dir)
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
}
