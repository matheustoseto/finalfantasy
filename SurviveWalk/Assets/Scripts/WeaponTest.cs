using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour {

    private Weapon weapon = null;

    [Header("Methold Test:")]
    [SerializeField] private TypeTool typeToolActiveTest = TypeTool.None;
    [SerializeField] private bool isMethold_SetActiveTool = false;

    // Use this for initialization
    void Start () {
        weapon = GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
        #region Test
        if (isMethold_SetActiveTool)
        {
            isMethold_SetActiveTool = false;
            weapon.SetActiveTool(typeToolActiveTest);
        }
        #endregion
    }
}
