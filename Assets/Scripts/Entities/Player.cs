using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GenericStateMachine<ECharacterState> StateMachine;
    [SerializeField] private PlayerInput playerInput;

    [Header("Animator")]
    public Animator characterAnimator;
    [Header("Mask Vars")]
    public EMaskType currentMask = EMaskType.None;
    public PlayerMovementData[] MaskMovement;
    public int currentMaskIndex = 0;
    public PlayerMovementData maskNoneMovement;
    [SerializeField] float switchTime = 0.2f;

    [Space]
    [Header("Bear Claw Attack Vars")]
    public Transform clawAttackPoint;
    public float clawAttackRadius;
    public float clawAttackAfterTime;
    public float clawAttackCooldown;
    public int clawDamage;

    [Header("Frog Tongue Attack Vars")]
    public Animator tongueAnimator;
    public Animator frogMaskAnimator;
    Coroutine tongueCoroutine;

    public Tongue tongue;
    public float tongueAttackCooldown;
    public int tongueDamage;

    [Header("Refs")]
    public Rigidbody rb;
    public Damageable damageable;
    public Transform graphicsTransform;
    public float prospettiveRotationDif = 50f;

    [Header("Movement Vars")]
    public Vector2 _moveInput;
    public Vector2 lastDir;
    public float speed;
    public float gravitiAdded;
    public float jumpForce;
    public float jumpCooldown;

    [Header("Bounce")]
    public float bounceOnEnemyForce = 2f;
    public int bounceDamage = 1;

    [Header("Dash Vars")]
    public float dashForce;
    public float dashDuration = 0.5f;
    [Tooltip("Il cooldown non può durare meno della dashDuration")]
    public float dashCooldown = 1f;

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector3 _groundCheckSize = new Vector3(1f, 0.5f, 1f);
    public bool IsFacingRight;
    public bool IsJumping;
    //public bool HasGoneUp = true;
    public bool IsMoving;
    public bool IsWalking;
    public bool IsGrounded;
    public bool IsFalling;
    //public bool IsSelectingMask;
    public bool IsDashing;
    public bool DashCooldown;
    public bool IsAttacking;

    [Space]
    public float SwitchMaskCoolDown = 0.2f;
    public bool IsSwitchingMask = true;
    public bool IsCatMask;
    public bool IsBearMask;
    public bool IsFrogMask;

    public bool debug;
    private void Awake()
    {
        GameManager.Instance.player = this;
        GameManager.Instance.playerInput = playerInput;

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
        //tongue.gameObject.SetActive(false);

        //tongue.OnColEnter += InterruptTongueAnim;
        SetMaskMovementData(MaskMovement[0]);

        damageable.onDeath.AddListener(() =>
        {
            GameManager.Instance.Respawn();
        });
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
        UpdateAnimVerticalSpeed();
        #endregion
        #region COLLISION CHECKS
        //se non sono in salto controllo se sono a terra
        //if (!IsJumping)
        //{
        //    //Ground Check
        //    if (Physics.CheckBox(_groundCheckPoint.position, _groundCheckSize, transform.rotation, _groundLayer))
        //    {
        //        IsGrounded = true;
        //    }
        //}
        #endregion
        #region JUMP CHECKS
        //se sono in salto e la velocità y è negativa (sto scendendo) allora non sono più in salto
        //if (IsJumping && rb.linearVelocity.y <= 0 && HasGoneUp)
        //{
        //    IsJumping = false;
        //}
        //if (!HasGoneUp && rb.linearVelocity.y > 0)
        //{
        //    Debug.Log("check");
        //    HasGoneUp = true;
        //}
        #endregion
    }
    private void FixedUpdate()
    {
        if (!IsDashing)
            Move();
    }
    private void OnEnable()
    {
        //assegno l'input del player
        playerInput.OnPlayerMoveAction += () =>
        {
            _moveInput.x = playerInput.MovementX;
            _moveInput.y = playerInput.MovementY;

            if (!IsDashing)
            {
                lastDir.x = playerInput.MovementX;
                lastDir.y = playerInput.MovementY;
            }

            CheckDirectionToFace(_moveInput.x > 0);

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
            if (CanDash())
                Dash();
        };

        playerInput.OnBearClawAttackAction += BearClawAttack; ;
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

            if (!IsDashing)
            {
                lastDir.x = playerInput.MovementX;
                lastDir.y = playerInput.MovementY;
            }

            CheckDirectionToFace(_moveInput.x > 0);

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
        playerInput.OnDashAction -= () =>
        {
            //controllo se posso dashare
            if (CanDash())
                Dash();
        };

        playerInput.OnBearClawAttackAction -= BearClawAttack; ;
        playerInput.OnFrogTongueAttackAction -= FrogTongueAttack;

        //playerInput.OnHoldSwitchMask -= SwitchMaskHold;
        //playerInput.OnUnHoldSwitchMask -= SwitchMaskUnHold;
        playerInput.OnSwitchLMask -= SwitchLeftMask;
        playerInput.OnSwitchRMask -= SwitchRightMask;
    }
    //funzione che verrà chiamata quando riavvio la scena per ricaricare perchè SONO MORTOOOO magari da pensarci poi
    public void SetupPlayer()
    {
        damageable.SetFullHealth();
    }
    #region CHECK METHODS
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("ho toccato " + col.gameObject.name);
        if (Physics.CheckBox(_groundCheckPoint.position, _groundCheckSize / 2, transform.rotation, _groundLayer))
        {
            IsJumping = false;
            IsGrounded = true;
            UpdateAnimIsGrounded();
        }
        //ho toccato un nemico
        if (((1 << col.gameObject.layer) & _enemyLayer) != 0)
        {
            Debug.Log("ho toccato un nemico");
            if (Physics.CheckBox(_groundCheckPoint.position, _groundCheckSize / 2, transform.rotation, _enemyLayer))
            {
                //sono saltato su di un nemico, allora rimbalzo e gli faccio danno
                rb.AddForce(new Vector3(0, bounceOnEnemyForce, 0), ForceMode.Impulse);
                Damageable dam = col.gameObject.GetComponent<Damageable>();
                if (dam != null)
                {
                    dam.TakeDamage(bounceDamage);
                }
                Debug.Log("gli faccio danno e rimbalzo");
            }
            else
            {
                //altrimenti prendo danno dal nemico
                damageable.DamageOnCollisionEnter(col);
                Debug.Log("mi fa danno");
            }
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (((1 << col.gameObject.layer) & _enemyLayer) != 0)
        {
            damageable.DamageOnCollisionExit(col);
        }
    }
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    public void Turn()
    {
        //stores scale and flips the player along the x axis, 
        //Vector3 scale = transform.localScale;
        //scale.x *= -1;
        //transform.localScale = scale;

        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180;
        transform.eulerAngles = rotation;

        Vector3 rotationGraphic = graphicsTransform.eulerAngles;
        if (IsFacingRight)
        {
            rotationGraphic.y -= prospettiveRotationDif*2;
            graphicsTransform.eulerAngles = rotationGraphic;
        }
        else
        {
            rotationGraphic.y += prospettiveRotationDif * 2;
            graphicsTransform.eulerAngles = rotationGraphic;
        }

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
        UpdateAnimSpeed();
    }
    #endregion
    #region JUMP METHODS
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);

        IsJumping = true;
        IsGrounded = false;
        UpdateAnimIsGrounded();
        //HasGoneUp = false;
    }
    private void JumpCut()
    {
        if (rb.linearVelocity.y > 0)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }
    #endregion
    #region DASH METHODS
    private void Dash()
    {
        StartCoroutine(StartDash());
    }
    private bool CanDash()
    {
        if (IsDashing || !IsCatMask || !IsSwitchingMask || DashCooldown)
            return false;
        else
            return true;
    }
    private IEnumerator StartDash()
    {
        IsDashing = true;
        UpdateAnimIsDashing();
        DashCooldown = true;
        float time = 0;
        while (time <= dashDuration)
        {
            time += Time.deltaTime;
            rb.linearVelocity = new Vector3(lastDir.normalized.x * dashForce, 0, 0);
            yield return null;
        }
        IsDashing = false;
        UpdateAnimIsDashing();
        yield return new WaitForSeconds(dashCooldown - dashDuration);
        DashCooldown = false;
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

        switch (mask.MovementName)
        {
            case "Cat":
                currentMask = EMaskType.Cat;
                Debug.Log("Current Mask set to Cat");
                IsCatMask = true;
                IsBearMask = false;
                IsFrogMask = false;
                break;
            case "Bear":
                currentMask = EMaskType.Bear;
                Debug.Log("Current Mask set to Bear");
                IsCatMask = false;
                IsBearMask = true;
                IsFrogMask = false;
                break;
            case "Frog":
                currentMask = EMaskType.Frog;
                Debug.Log("Current Mask set to Frog");
                IsCatMask = false;
                IsBearMask = false;
                IsFrogMask = true;
                break;
            default:
                currentMask = EMaskType.None;
                break;
        }
        StartCoroutine(MaskSwitchCooldown());
    }
    IEnumerator MaskSwitchCooldown()
    {
        IsSwitchingMask = false;
        yield return new WaitForSeconds(SwitchMaskCoolDown);
        IsSwitchingMask = true;
    }
    private void SwitchLeftMask()
    {
        if (!IsSwitchingMask)
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
        if (!IsSwitchingMask || IsAttacking || IsDashing)
            return;

        currentMaskIndex++;
        if (currentMaskIndex >= MaskMovement.Length)
        {
            currentMaskIndex = 0;
        }
        SetMaskMovementData(MaskMovement[currentMaskIndex]);
        UIManager.Instance.SwitchToRightMask();
    }
    #endregion
    #region ATTACKS METHODS

    private void BearClawAttack()
    {
        //posso attaccare solo se non sto già attaccando
        if (IsAttacking || !IsBearMask || !IsSwitchingMask)
            return;

        StartCoroutine(ClawAttackCoroutine());
    }
    IEnumerator ClawAttackCoroutine()
    {
        IsAttacking = true;

        yield return new WaitForSeconds(clawAttackAfterTime);
        Debug.Log("Bear Claw Attack");
        //faccio un overlap sphere per vedere se colpisco qualcosa
        Collider[] hitColliders = Physics.OverlapSphere(clawAttackPoint.position, clawAttackRadius, _enemyLayer);
        foreach (var hit in hitColliders)
        {
            Debug.Log("Hit: " + hit.name);
            Damageable damageable = hit.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(clawDamage);
            }
        }
        yield return new WaitForSeconds(clawAttackCooldown);
        IsAttacking = false;
    }
    private void FrogTongueAttack()
    {
        return;
        //damageable fixare quando avrò i models
        //posso attaccare solo se non sto già attaccando
        if (IsAttacking || IsDashing || !IsSwitchingMask)
            return;

        tongueCoroutine = StartCoroutine(TongueAttackCoroutine());
    }
    IEnumerator TongueAttackCoroutine()
    {
        IsAttacking = true;
        tongue.gameObject.SetActive(true);
        //a seconda della dir faccio determinata animazione
        tongueAnimator.SetTrigger("AttackTrigger");
        frogMaskAnimator.SetTrigger("AttackTrigger");

        //mi prendo la durata dell'animazione di tongue attack
        float tongueAttackDuration = tongueAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(tongueAttackDuration / 2); //aspetto metà animazione per attivare la lingua
        //ha raggiunto la fine dell'estensione, quindi torno a casa

        tongue.gameObject.SetActive(false);

        yield return new WaitForSeconds(tongueAttackCooldown);

        IsAttacking = false;
    }
    public void InterruptTongueAnim()
    {
        if (tongueCoroutine != null)
        {
            StopCoroutine(tongueCoroutine);
            tongueCoroutine = null;
        }

        AnimatorStateInfo info = tongueAnimator.GetCurrentAnimatorStateInfo(0);
        float interruptedAnimTime = info.normalizedTime % 1f;

        tongueAnimator.SetFloat("StopFrame", interruptedAnimTime);
        tongueAnimator.SetTrigger("HitTrigger");

        frogMaskAnimator.SetFloat("StopFrame", interruptedAnimTime);
        frogMaskAnimator.SetTrigger("HitTrigger");

        tongue.gameObject.SetActive(false);
        //passo il parametro del frame in cui interrompo l'animazione

    }
    #endregion
    #region ANIMATOR UPDATES
    private void UpdateAnimSpeed()
    {
        characterAnimator.SetFloat("speed", rb.linearVelocity.magnitude);
    }
    private void UpdateAnimIsGrounded()
    {
        characterAnimator.SetBool("isGrounded", IsGrounded);
    }
    private void UpdateAnimVerticalSpeed()
    {
        characterAnimator.SetFloat("verticalSpeed", rb.linearVelocity.y);
    }
    private void UpdateAnimIsDashing()
    {
        characterAnimator.SetBool("isDashing", IsDashing);
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
