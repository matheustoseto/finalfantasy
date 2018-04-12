using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimerUI : MonoBehaviour {

    [SerializeField] private RectTransform clockPointerUI = null;
    [SerializeField] private Text days = null;

    public Vector3 Rotation { get { return clockPointerUI.eulerAngles; } set { clockPointerUI.eulerAngles = value; } }
    public string Days { get { return days.text; } set { days.text = value; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
