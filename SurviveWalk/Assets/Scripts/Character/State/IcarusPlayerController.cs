using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMoveControl))]
public class IcarusPlayerController : CharacterState {

    public static Transform player = null;

    private CharacterMoveControl moveControl = null;
    private CharacterAnimationControl aniControl = null;

    [Header("Inputs:")]
    [SerializeField] private Vector2 btDirection = Vector2.zero;
    [SerializeField] private float   btAttack  = 0;
    [SerializeField] private float   btAction = 0;
    [SerializeField] private float   btDash    = 0;

    private bool isAnimation = false;

    [Header("Debug:")]
    [SerializeField] private bool isAttackDb = false;

    private float timer, timerFinish = 0;

    public static Transform GetInstance() { return player; }

    // Use this for initialization

    private void Awake()
    {
        if (player == null)
            player = transform;
    }

    void Start () {
        moveControl = GetComponent<CharacterMoveControl>();

        if (aniControl == null)
            aniControl = GetComponent<CharacterAnimationControl>();
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
    }

    #region EnterState
    protected override void EnterMoveState()
    {
        isAnimation = false;
        //moveControl.Stop();
    }

    protected override void EnterAttackState()
    {
        timer = 0;
        timerFinish = 0.6f;
        isAnimation = false;
        moveControl.Stop();
    }

    protected override void EnterDashState()
    {
        isAnimation = false;
        moveControl.Stop();
    }

    protected override void EnterActionState()
    {
        isAnimation = false;
        moveControl.Stop();
    }

    #endregion

    #region UpdateState

    protected override void UpdateMoveState()
    {
        //moveControl.Move(btDirection);

        // ANIMATION //
        aniControl.Locomotion();


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

        // ANIMATION //
        //bool isAttack = aniControl.IsAnimationCurrentName(TypeStateCharacter.Attack);
        //bool isAttack = aniControl.IsPlayingCurrentAnimatorState();

        if (!isAnimation)
        {
            aniControl.Attack();
            //isAttack = true;
            isAnimation = true;
        }


        isAttackDb = isAnimation;

        //if (!isAttack)
        //Debug.Log(aniControl.IsPlayingCurrentAnimatorState());
        //Debug.Log(aniControl.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        //if (!aniControl.Animator.GetCurrentAnimatorStateInfo(0).IsName("Atttack"))

        //if (!aniControl.IsAttack)
        if(aniControl.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            aniControl.Animator.SetBool("Attack", false);
            isAnimation = false;
            // CHANGE STATE //
            EnterState(TypeStateCharacter.Move);
            return;
        }

        // ANIMATION //
        //bool isAttack = aniControl.IsAnimationCurrentName(TypeStateCharacter.Attack);
        //bool isAttack = aniControl.IsPlayingCurrentAnimatorState();

        //if (!isAnimation)
        //{
        //    aniControl.Attack();
        //    isAnimation = true;
        //}

        //if (!aniControl.Animator.get)
        //{
        //    EnterState(TypeStateCharacter.Move);
        //}

    }

    protected override void UpdateDashState()
    {
        bool isDash = aniControl.IsAnimationCurrentName(TypeStateCharacter.Dash); 
        if (btDash != 0)
        {
            isDash = aniControl.Dash();
            isDash = true;
        }

        if (!isDash)
        {
            EnterState(TypeStateCharacter.Move);
        }
    }

    protected override void UpdateActionState()
    {
        bool isAction = aniControl.IsAnimationCurrentName(TypeStateCharacter.Action);
        if (btDash != 0)
        {
            isAction = aniControl.Action();
            isAction = true;
        }

        if (!isAction)
        {
            EnterState(TypeStateCharacter.Move);
        }
    }


    #endregion

    #region LeaveState
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
