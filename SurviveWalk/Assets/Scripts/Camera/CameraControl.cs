using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : CameraFollow {

    
    [SerializeField] private List<Vector3> distancePositions = new List<Vector3>();
    [SerializeField] private int posDistPosition = 0;
    [SerializeField] private Vector3 distancePoint = Vector3.zero;



    public Vector3 DistancePoint { get { return distancePoint; }
        set
        {
            Vector3 auxDistancePoint = new Vector3(Mathf.Abs(value.x),
                                                   Mathf.Abs(value.y),
                                                  -Mathf.Abs(value.z));
            distancePoint = auxDistancePoint;
        }
    }

    private float inputCameraPosition = 0;

    // Use this for initialization
    void Start () {
        if (distancePositions.Count == 0)
            DistancePoint = distancePoint;
        else
            DistancePoint = distancePositions[posDistPosition];
	}
	
    private void GetInputs()
    {
        //inputCameraPosition = PlayerControl.playerInput.CameraPositionKeyboard;

        inputCameraPosition = Input.GetKeyDown(KeyCode.PageDown) ? -1 : Input.GetKeyDown(KeyCode.PageUp) ? 1 : 0;
    }

	// Update is called once per frame
	void Update () {
        GetInputs();
	}

    private void LateUpdate()
    {
        Follow();

        if (distancePositions.Count > 0)
        {
            if (inputCameraPosition > 0 && posDistPosition + 1 < distancePositions.Count) // Page Up
            {
                posDistPosition++;
                DistancePoint = distancePositions[posDistPosition];
            }
            else if (inputCameraPosition < 0 && posDistPosition - 1 >= 0) // Page Down
            {
                posDistPosition--;
                DistancePoint = distancePositions[posDistPosition];
            }
        }
        LootAt();
    }


    private void LootAt()
    {
        transform.LookAt(TargetObject.GetTransform().position);
    }

    #region Target
    protected override void Follow()
    {
        Vector3 auxDistancePoint = DistancePoint;
        auxDistancePoint.x = 0;

        transform.position = TargetObject.GetTransform().position + auxDistancePoint;
    }
    #endregion
}
