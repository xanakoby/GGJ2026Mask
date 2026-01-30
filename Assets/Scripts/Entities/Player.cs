using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GenericStateMachine<ECharacterState> StateMachine;
    [SerializeField] private PlayerInput playerInput;

    [Header("Mask Vars")]
    public EMaskType currentMask = EMaskType.None;
    public PlayerMovementData[] MaskMovement;
    public int currentMaskIndex = 0;
    public PlayerMovementData maskNoneMovement;
    [SerializeField] float switchTime = 0.2f;

    [Space]
    public LayerMask enemyMask;

    [Header("Bear Claw Attack Vars")]
    public Transform clawAttackPoint;
    public float clawAttackRadius;
    public float clawAttackAfterTime;
    public float clawAttackCooldown;
    public int clawDamage;

    [Header("Frog Tongue Attack Vars")]
    public float tongueAttackCooldown;
    public int tongueDamage;

    [Header("Refs")]
    public Rigidbody rb;

    [Header("Movement Vars")]
    public Vector2 _moveInput;
    public float speed;
    public float gravitiAdded;
    public float jumpForce;

    [Header("Dash Vars")]
    public float dashForce;
    public float dashCooldown;
    public float dashDuration;

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector3 _groundCheckSize = new Vector3(1f, 0.5f, 1f);
    public bool IsFacingRight;
    public bool IsJumping { get; private set; }
    public bool IsMoving;
    public bool IsWalking;
    public bool IsGrounded;
    public bool IsFalling;
    //public bool IsSelectingMask;
    public bool IsDashing;
    public bool IsAttacking;

    [Space]
    public float SwitchMaskCoolDown = 0.2f;
    public bool CanSwitchMask = true;
    public bool IsCatMask;
    public bool IsBearMask;
    public bool IsFrogMask;

    public bool debug;
    private void Awake()
    {
        StateMachine = new GenericStateMachine<ECharacterState>();

        StateMachine.RegisterState(ECharacterState.Idle, new IdleCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Walking, new WalkingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Jumping, new JumpingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Falling, new FallingCharacterState(this));
        //StateMachine.RegisterState(ECharacterState.Landing, new LandingCharacterState(this));

        SetState(ECharacterState.Idle);
    }
    private void Start()
    {
        //debugging?
        SetMaskMovementData(MaskMovement[0]);
    }

    public void SetState(ECharacterState newState)
    {
        StateMachine.SetState(newState);
    }

    void Update()
    {
        StateMachine.OnUpdate();

        if (_moveInput.x != 0)
        {
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }

        #region GRAVITY
        rb.AddForce(Vector3.down * gravitiAdded, ForceMode.Acceleration);
        #endregion
        #region COLLISION CHECKS
        //se non sono in salto controllo se sono a terra
        if (!IsJumping)
        {
            //Ground Check
            if (Physics.CheckBox(_groundCheckPoint.position, _groundCheckSize, transform.rotation, _groundLayer))
            {
                IsGrounded = true;
            }
        }
        #endregion
        #region JUMP CHECKS
        //se sono in salto e la velocità y è negativa (sto scendendo) allora non sono più in salto
        if (IsJumping && rb.linearVelocity.y < 0)
        {
            IsJumping = false;
        }
        #endregion
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnEnable()
    {
        //assegno l'input del player
        playerInput.OnPlayerMoveAction += () =>
        {
            _moveInput.x = playerInput.MovementX;
            _moveInput.y = playerInput.MovementY;

            IsMoving = true;
        };
        playerInput.OnPlayerStandAction += () =>
        {
            _moveInput.x = 0;
            _moveInput.y = 0;
            IsMoving = false;
        };

        playerInput.OnPlayerJumpAction += () =>
        {
            //if (IsSelectingMask)
            //    return;

            if (CanJump())
                Jump();
        };
        playerInput.OnPlayerStopHoldJumpAction += () =>
        {
            //if (IsSelectingMask)
            //    return;

            if (CanJumpCut())
                JumpCut();
        };
        playerInput.OnDashAction += () =>
        {
            //controllo se posso dashare
        };

        playerInput.OnBearClawAttackAction += BearClawAttack;;
        playerInput.OnFrogTongueAttackAction += FrogTongueAttack;

        //playerInput.OnHoldSwitchMask += SwitchMaskHold;
        //playerInput.OnUnHoldSwitchMask += SwitchMaskUnHold;
        playerInput.OnSwitchLMask += SwitchLeftMask;
        playerInput.OnSwitchRMask += SwitchRightMask;
    }
    private void OnDisable()
    {
        //disassegno l'input del player
        playerInput.OnPlayerMoveAction -= () =>
        {
            _moveInput.x = playerInput.MovementX;
            _moveInput.y = playerInput.MovementY;
            IsMoving = true;
        };
        playerInput.OnPlayerStandAction -= () =>
        {
            _moveInput.x = 0;
            _moveInput.y = 0;
            IsMoving = false;
        };

        playerInput.OnPlayerJumpAction -= () =>
        {
            //if (IsSelectingMask)
            //    return;

            if (CanJump())
                Jump();
        };
        playerInput.OnPlayerStopHoldJumpAction -= () =>
        {
            //if (IsSelectingMask)
            //    return;

            if (CanJumpCut())
                JumpCut();
        };

        //playerInput.OnHoldSwitchMask -= SwitchMaskHold;
        //playerInput.OnUnHoldSwitchMask -= SwitchMaskUnHold;
        playerInput.OnSwitchLMask -= SwitchLeftMask;
        playerInput.OnSwitchRMask -= SwitchRightMask;
    }
    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    public void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }
    private bool CanJump()
    {
        return IsGrounded && !IsJumping;
    }
    private bool CanJumpCut()
    {
        return IsJumping && rb.linearVelocity.y > 0;
    }
    #endregion
    #region GENERAL METHODS
    public void SetGravityScale(float addForce)
    {
        gravitiAdded = addForce;
    }
    #endregion
    #region MOVEMENT METHODS
    private void Move()
    {
        rb.linearVelocity = new Vector3(_moveInput.x * speed, rb.linearVelocity.y, rb.linearVelocity.z);
    }
    #endregion
    #region JUMP METHODS
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

        IsJumping = true;
        IsGrounded = false;
    }
    private void JumpCut()
    {
        if(rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }
    #endregion
    #region MASK METHODS
    //private void SwitchMaskHold()
    //{
    //    //fermo il tempo e per ora apro il menu delle maschere
    //    Time.timeScale = 0f;
    //    UIManager.Instance.ShowMaskSelectionMenu();

    //    IsSelectingMask = true;
    //}
    //private void SwitchMaskUnHold()
    //{
    //    //il tempo riprende e chiudo il menu delle maschere
    //    Time.timeScale = 1f;
    //    UIManager.Instance.HideMaskSelectionMenu();

    //    IsSelectingMask = false;
    //}
    public void SetMaskMovementData(PlayerMovementData mask)
    {
        speed = mask.Speed;
        jumpForce = mask.JumpForce;
        gravitiAdded = mask.GravityAdded;

        switch(mask.MovementName)
        {
            case "Cat":
                currentMask = EMaskType.Cat;
                    Debug.Log("Current Mask set to Cat");
                break;
            case "Bear":
                currentMask = EMaskType.Bear;
                Debug.Log("Current Mask set to Bear");
                break;
            case "Frog":
                currentMask = EMaskType.Frog;
                Debug.Log("Current Mask set to Frog");
                break;
            default:
                currentMask = EMaskType.None;
                break;
        }
        StartCoroutine(MaskSwitchCooldown());
    }
    IEnumerator MaskSwitchCooldown()
    {
        CanSwitchMask = false;
        yield return new WaitForSeconds(SwitchMaskCoolDown);
        CanSwitchMask = true;
    }
    private void SwitchLeftMask()
    {
        if (!CanSwitchMask)
            return;
        if (IsAttacking)
            return;

        currentMaskIndex--;
        if (currentMaskIndex < 0)
        {
            currentMaskIndex = MaskMovement.Length - 1;
        }
        SetMaskMovementData(MaskMovement[currentMaskIndex]);
        UIManager.Instance.SwitchToLeftMask();
    }
    private void SwitchRightMask()
    {
        if (!CanSwitchMask)
            return;
        if (IsAttacking)
            return;

        currentMaskIndex++;
        if (currentMaskIndex >= MaskMovement.Length)
        {
            currentMaskIndex = 0;
        }
        SetMaskMovementData(MaskMovement[currentMaskIndex]);
        UIManager.Instance.SwitchToRightMask();
    }

    private void BearClawAttack()
    {
        //posso attaccare solo se non sto già attaccando
        if (IsAttacking)
            return;

        StartCoroutine(ClawAttackCoroutine());
    }
    IEnumerator ClawAttackCoroutine()
    {
        IsAttacking = true;

        yield return new WaitForSeconds(clawAttackAfterTime);
        Debug.Log("Bear Claw Attack");
        //faccio un overlap sphere per vedere se colpisco qualcosa
        Collider[] hitColliders = Physics.OverlapSphere(clawAttackPoint.position, clawAttackRadius, enemyMask);
        foreach (var hit in hitColliders)
        {
            Damageable damageable = hit.GetComponent<Damageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(clawDamage);
            }
        }
        yield return new WaitForSeconds(clawAttackCooldown);
        IsAttacking = false;
    }
    private void FrogTongueAttack()
    {
        //posso attaccare solo se non sto già attaccando
        if (IsAttacking)
            return;

        StartCoroutine(TongueAttackCoroutine());
    }
    IEnumerator TongueAttackCoroutine()
    {
        IsAttacking = true;
        yield return new WaitForSeconds(tongueAttackCooldown);
        IsAttacking = false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(clawAttackPoint.position, clawAttackRadius);
    }
}
