using UnityEngine;
using System;

[Serializable]
public class GameInput
{
    [SerializeField] private string joystickNames = "";
    [SerializeField] private string forward = "Vertical";
    [SerializeField] private string turn    = "Horizontal";
    [SerializeField] private string action  = "Action";
    [SerializeField] private string cameraPosition = "CameraPosition";
    [SerializeField] string joyName = "";

    public string JoystickNames { get { return joystickNames; } set { joystickNames = value; } }
    public string JoyReference { get { return joyName; } set { joyName = value; } }

    public float Forward            { get { return Input.GetAxis( joyName + forward); } }
    public float Turn               { get { return Input.GetAxis( joyName + turn); } }
    public float Action             { get { return Input.GetAxis( joyName + action); } }
    public float CameraPosition { get { return Input.GetAxis( joyName + cameraPosition); } }
    public float CameraPositionKeyboard { get { return Input.GetKeyDown(KeyCode.PageDown) ? -1 : Input.GetKeyDown(KeyCode.PageUp) ? 1 : 0 ; } }


}
