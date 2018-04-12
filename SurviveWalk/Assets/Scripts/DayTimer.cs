using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour {

    [SerializeField] private Transform sun = null;
    [SerializeField] private Light day = null;
    [SerializeField] private Light night = null;
    [SerializeField] private DayTimerUI uiDayTimer = null;
    [SerializeField] private int days = 0;
    [SerializeField] private float speedTime = 1;
    
    float iniRotation = 0;
    float actRotation = 0;

    

	// Use this for initialization
	void Start () {
        sun = GetComponent<Transform>();

        Light[] lights = GetComponentsInChildren<Light>();
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i].name == "Day")
                day = lights[i];
            else
                night = lights[i];
        }



        sun.eulerAngles = new Vector3(Mathf.Abs(sun.eulerAngles.x), sun.eulerAngles.y, sun.eulerAngles.z);



        iniRotation = sun.eulerAngles.x;

        uiDayTimer.Rotation = new Vector3(0, 0, -iniRotation);
        uiDayTimer.Days = days.ToString();

        actRotation = iniRotation;


        if (actRotation > 180)
        {
            day.gameObject.SetActive(false);
            night.gameObject.SetActive(true);
        }
        else
        {
            day.gameObject.SetActive(true);
            night.gameObject.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void LateUpdate () {
        SunRotationCalc();
        SunRotationUpdate();
        UIUpdate();
    }

    private void SunRotationCalc()
    {
        float errorLimit = 1;

        actRotation += speedTime * Time.deltaTime;
        if (actRotation >= 360)
        {
            actRotation = 0;
            days++;

            day.gameObject.SetActive(true);
            night.gameObject.SetActive(false);
        }
        else if (actRotation >= 180 - errorLimit && actRotation <= 180 + errorLimit)
        {
            day.gameObject.SetActive(false);
            night.gameObject.SetActive(true);
        }

    }

    private void SunRotationUpdate()
    {
        if (sun != null)
        {
            sun.eulerAngles = new Vector3(actRotation, 0, 0);
        }

    }



    //[SerializeField] private bool IsLightIntensity = false;
    //private Light light = null;
    //light = GetComponent<Light>();
    //    if (IsLightIntensity)
    //        light.intensity = Mathf.Abs(sun.eulerAngles.x) / 360;
    //SunLightUpdate();
    //private void SunLightUpdate()
    //{

    //    if (light != null && IsLightIntensity)
    //    {

    //        float intensity = actRotation / 360;

    //        if (intensity < 0.5)
    //            light.intensity = actRotation / 180;
    //        else
    //        {
    //            light.intensity = (360 - actRotation) / 180;
    //        }
    //    }
    //}

    private void UIUpdate()
    {
        if (uiDayTimer != null)
        {
            uiDayTimer.Rotation = new Vector3(0, 0, -actRotation);

            if (actRotation == 0)
                uiDayTimer.Days = days.ToString();
        }
    }
}
