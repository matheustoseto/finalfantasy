using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheat : MonoBehaviour {
    [SerializeField] private CharacterMoveControl moveControl = null;

    [SerializeField] private List<Transform> listCheckPoints = new List<Transform>();
    [SerializeField] private int actualCheckPoint = 0;

    // Use this for initialization
    void Start () {
        moveControl = GetComponent<CharacterMoveControl>();

        if (listCheckPoints.Count != 0)
        {
            for (int i = 0; i < listCheckPoints.Count; i++)
            {
                if (listCheckPoints[i] == null)
                {
                    listCheckPoints.Clear();
                    GameObject gobj = new GameObject();
                    gobj.transform.position = transform.position;
                    gobj.transform.rotation = transform.rotation;
                    listCheckPoints.Add(gobj.transform);
                    break;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (actualCheckPoint + 1 >= listCheckPoints.Count)
                actualCheckPoint = 0;
            else
                actualCheckPoint++;
            moveControl.ReturnCheckPoint(listCheckPoints[actualCheckPoint].position);
        }
	}
}
