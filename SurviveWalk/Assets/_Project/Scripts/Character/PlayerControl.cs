using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    public static GameInput playerInput = null;
    public static bool IsDebug = true;

    private MeshRenderer hitBox = null;


    [Header("Inputs:")]
    [SerializeField] private GameInput gameInput = new GameInput();

    private MoveControl moveControl;

    private float inputForward = 0;
    private float inputTurn = 0;
    private float inputAction = 0;


    void Awake()
    {
        playerInput = gameInput;


        // Joystick Reference 
        string[] ginputs = Input.GetJoystickNames();
        for (int i = 0; i < ginputs.Length; i++)
        {
            if (ginputs[i] !="" &&  ginputs[i].ToUpper().Contains("XBOX"))
            {
                gameInput.JoyReference = "XBOX_1_";
                gameInput.JoystickNames = ginputs[i];
                break;
            }
        }

        if (ginputs.Length > 0 && ginputs[0] != "")
        {
            gameInput.JoyReference = "GENERIC_1_";
            gameInput.JoystickNames = ginputs[0];
        }


        //// Debug //
        //hitBox = Util.SearchChildren(transform, "Hitbox", TypeSeach.name).GetComponent<MeshRenderer>();
        //if (hitBox != null)
        //    hitBox.enabled = IsDebug;

    }


    // Use this for initialization
    void Start()
    {
        moveControl = GetComponent<MoveControl>();
    }

    private void GetInputs()
    {
        inputForward = gameInput.Forward;
        inputTurn = gameInput.Turn;
        inputAction = gameInput.Action;
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        moveControl.Move(inputForward, inputTurn);

        // Debug //
        if (IsDebug)
        {
            if (hitBox != null)
                hitBox.enabled = IsDebug;
        }
    }

}
