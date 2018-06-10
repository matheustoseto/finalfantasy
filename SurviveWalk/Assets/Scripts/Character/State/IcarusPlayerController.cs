using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMoveControl))]
public class IcarusPlayerController : CharacterState {

    public static Transform player = null;

    private CharacterMoveControl moveControl = null;
    private CharacterAnimationControl aniControl = null;

    [Header("Tools / Weapon:")]
    [SerializeField] private Weapon weapon;

    [Header("Inputs:")]
    [SerializeField] private Vector2 btDirection = Vector2.zero;
    [SerializeField] private float   btAttack    = 0;
    [SerializeField] private float   btAction    = 0;
    [SerializeField] private float   btDash      = 0;


    [Header("Debug:")]
    [SerializeField] private bool isAttackDb = false;


    public static Transform GetInstance() { return player; }

    #region Properties
    public Weapon Weapon { get { return weapon; } }
    #endregion

    // Use this for initialization

    private void Awake()
    {
        if (player == null)
            player = transform;

        moveControl = GetComponent<CharacterMoveControl>();

        if (aniControl == null)
            aniControl = GetComponentInChildren<CharacterAnimationControl>();

        if (weapon == null)
        {
            weapon = GetComponentInChildren<Weapon>();
            aniControl.Weapon = weapon;
        }
    }

    void Start () {
        EnterState(state);
    }


    private void GetInput()
    {
        btDirection.x = Input.GetAxisRaw   ("Horizontal");
        btDirection.y = Input.GetAxisRaw   ("Vertical"  );
        btAttack      = Input.GetButtonDown("Fire1"     ) ? 1 : 0;
        btAction      = Input.GetButtonDown("Action"    ) ? 1 : 0;
        btDash        = Input.GetButtonDown("Fire2"     ) ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        UpdateState();
        aniControl.ExecuteAnimations();
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
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateDashState()
    {
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateActionState()
    {
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
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

    #endregion

}
