using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAnimationControl : MonoBehaviour {

    private EnemyAnimationControl aniControl;

    [SerializeField] private float SpeedPercent   = 0;
    [SerializeField] private bool  IsAttackTest   = false;
    [SerializeField] private bool  IsDashTest     = false;
    [SerializeField] private bool  IsActionTest   = false;
    [SerializeField] private bool  IsDeadTest     = false;
    [SerializeField] private bool  IsRiseTest     = false;
    [SerializeField] private bool  IsFakeDeadTest = false;
    [SerializeField] private bool  IsFallTest     = false;

    // Use this for initialization
    void Start () {
        aniControl = GetComponent<EnemyAnimationControl>();
	}
	
	// Update is called once per frame
	void Update () {
        aniControl.SpeedPercent = SpeedPercent;
        aniControl.IsAttack   = IsAttackTest   ;
        aniControl.IsDash     = IsDashTest     ;
        aniControl.IsAction   = IsActionTest   ;
        aniControl.IsDead     = IsDeadTest     ;
        aniControl.IsRise     = IsRiseTest     ;
        aniControl.IsFakeDead = IsFakeDeadTest ;
        aniControl.IsFall     = IsFallTest     ;

        aniControl.ExecuteAnimations();
    }
}
