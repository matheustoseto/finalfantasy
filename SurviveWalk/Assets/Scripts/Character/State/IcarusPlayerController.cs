using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMoveControl))]
public class IcarusPlayerController : CharacterState {

    public static Transform player = null;
    private CharacterStatus playerStatus = null;

    private CharacterMoveControl moveControl = null;
    private CharacterAnimationControl aniControl = null;

    [Header("Methold Test: ")]
    [SerializeField] private bool isAutomaticResurrection = true;
    [SerializeField] private bool isResurrection = false;

    //[Header("Inputs:")]
    private Vector2 btDirection = Vector2.zero;
    private float   btAttack    = 0;
    private float   btAction    = 0;
    private float   btDash      = 0;


    [Header("Debug:")]
    [SerializeField] private bool isAttackDb = false;


    public static Transform GetInstance() { return player; }

    #region Properties
    
    #endregion

    // Use this for initialization

    private void Awake()
    {
        InitAwake();
    }

    protected override void InitAwake()
    {
        base.InitAwake();
        if (player == null)
            player = transform;

        moveControl = GetComponent<CharacterMoveControl>();

        if (aniControl == null)
            aniControl = GetComponentInChildren<CharacterAnimationControl>();

        if (aniControl != null)
            aniControl.Weapon = Weapon;
        else
            Debug.Log("[IcarusPlayerController][InitAwake]: CharacterAnimationControl is null!");

    }

    void Start () {
        playerStatus = GetComponent<CharacterStatus>();
        EnterState(state);
    }


    private void GetInput()
    {
        btDirection.x = Input.GetAxisRaw   ("Horizontal");
        btDirection.y = Input.GetAxisRaw   ("Vertical"  );
        btAttack      = Input.GetButtonDown("Fire1"     ) ? 1 : 0;
        btAction      = Input.GetButtonDown("Action"    ) ? 1 : 0;
        //btDash        = Input.GetButtonDown("Fire2"     ) ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        UpdateState();
        aniControl.ExecuteAnimations();

        #region Test
        if (isResurrection)
        {
            isResurrection = false;
            Resurrection();
        }
        #endregion
    }

    #region EnterState
    protected override void EnterMoveState()
    {
        // ANIMATION //
        aniControl.IsLocomotion = true;

    }

    protected override void EnterAttackState()
    {
        aniControl.IsAttack = true;
        moveControl.Stop();
    }

    protected override void EnterDashState()
    {
        aniControl.IsDash = true;
        moveControl.Stop();
    }

    protected override void EnterActionState()
    {
        aniControl.IsAction = true;
        moveControl.Stop();
    }

    protected override void EnterDeadState()
    {
        aniControl.IsDead = true;
    }

    #endregion

    #region UpdateState

    protected override void UpdateMoveState()
    {
        moveControl.Move(btDirection);
        aniControl.SpeedPercent = moveControl.Magnitude / 2;


        // CHANGE STATE //
        if (btAttack != 0)
        {
            EnterState(TypeStateCharacter.Attack);
            return;
        }

        if (btAction != 0)
        {
            EnterState(TypeStateCharacter.Action);
            return;
        }

        if (btDash != 0)
        {
            EnterState(TypeStateCharacter.Dash);
            return;
        }
    }

    protected override void UpdateAttackState()
    {
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateDashState()
    {
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateActionState()
    {
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateDeadState()
    {
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            if (isAutomaticResurrection)
                EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    #endregion

    #region LeaveState
    protected override void LeaveState()
    {
        base.LeaveState();
        aniControl.Release();
    }

    protected override void LeaveMoveState()
    {
        
    }

    protected override void LeaveAttackState()
    {
        
    }

    protected override void LeaveDashState()
    {
        
    }

    protected override void LeaveActionState()
    {
        
    }

    protected override void LeaveDeadState()
    {
        if(isAutomaticResurrection)
            Resurrection();
    }
    #endregion

    public void Resurrection()
    {
        playerStatus.ResetToInitialLife();
        moveControl.ReturnCheckPoint();
    }
}
