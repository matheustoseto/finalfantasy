using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMoveControl : MonoBehaviour {

    private CharacterController charController = null;
    private NavMeshAgent navAgent = null;

    [Header("Attributes:")]
    [SerializeField] private float runSpeed        = 10  ;
    [SerializeField] private float speedSmoothTime = 0.1f;
    [SerializeField] private Transform checkPoint = null;

    private float currentSpeed        = 0;
    private float speedSmoothVelocity = 0;


    [Header("Reference:")]
    [SerializeField] private GameObject bodyMiniMap = null;
    [SerializeField] private Transform body = null;


    #region Properties

    public Transform  Body        { get { return body;        } }
    public GameObject BodyMiniMap { get { return bodyMiniMap; } }
    public float Magnitude { get { return charController.velocity.magnitude; } }

    #endregion


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        transform.position = checkPoint.position;
    }

    // Use this for initialization
    void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.Stop();

        if (bodyMiniMap != null)
            bodyMiniMap.gameObject.SetActive(true);
    }

    
    public void Move(Vector2 dir)
    {
        Vector2 inputDir = dir.normalized;

        //if (inputDir != Vector2.zero)
        //{
        //    float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
        //    transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        //}
        //
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
        charController.Move(move);
    }

    public void Stop()
    {
        Vector3 move = transform.TransformDirection(Vector3.zero);
        charController.Move(move);
    }


    private void BodyRotateKeyboard(float inputForward, float inputTurn)
    {
        // Efetuar  rotação em Y
        //if (inputForward == 0 && inputTurn == 0) body.transform.eulerAngles = new Vector3(0, 180, 0); // Center
        //else
        if      (inputForward >  0 && inputTurn == 0) body.transform.eulerAngles = new Vector3(0,  0,  0); // Up
        else if (inputForward >  0 && inputTurn >  0) body.transform.eulerAngles = new Vector3(0, 45,  0); // UpRight
        else if (inputForward == 0 && inputTurn >  0) body.transform.eulerAngles = new Vector3(0, 90,  0); // Right
        else if (inputForward <  0 && inputTurn >  0) body.transform.eulerAngles = new Vector3(0, 135, 0); // DownRight
        else if (inputForward <  0 && inputTurn == 0) body.transform.eulerAngles = new Vector3(0, 180, 0); // Down
        else if (inputForward <  0 && inputTurn <  0) body.transform.eulerAngles = new Vector3(0, 225, 0); // DownLeft
        else if (inputForward == 0 && inputTurn <  0) body.transform.eulerAngles = new Vector3(0, 270, 0); // Left
        else if (inputForward >  0 && inputTurn <  0) body.transform.eulerAngles = new Vector3(0, 315, 0); // UpLeft
    }

    #region Restat Position
    public void ReturnCheckPoint()
    {
        Vector3 posCheckpoint = checkPoint.position;
        posCheckpoint.y = 10000;

        navAgent.enabled = false;
        transform.position = posCheckpoint;
        transform.position = checkPoint.position;
        navAgent.enabled = true;
    }
    #endregion
}
