using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Controller : MonoBehaviour, IDamageable, IComparable<Robot_Controller>
{
    [Header("Stats")]
    [SerializeField] public Character_Data robotData;
    [SerializeField] private Transform startPosition;

    [Header("References")]
    [SerializeField] public PlayerHUDController playerHUD;
    [SerializeField] private Animator animator;
    [SerializeField] public RobotAnimatorController robotAnimatorController;
    [SerializeField] private CameraControl cameraControl;

    [Header("Audio sources")]
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource attackAudioSource;
    [SerializeField] private AudioSource takeDamageAudioSource;

    [Header("Transforms")]
    [SerializeField] private Transform impactEffectTransform;

    [Header("Debug")]
    [SerializeField] private int moveDirection;

    // Properties
    public float Health { get; private set; }
    public bool IsDefending { get; set; }
    public bool IsDefeated { get; private set; }
    public int WinCount { get; set; }
    public float MoveInput { get; set; }

    // State machine
    public Robot_StateMachine RobotStateMachine { get; private set; }
    public Robot_IdleState RobotIdleState { get; private set; }
    public Robot_WalkState RobotWalkState { get; private set; }
    public Robot_AttackState RobotAttackState { get; private set; }
    public Robot_DefenseState RobotDefenseState { get; private set; }


    // Components
    private Rigidbody rb;

    // Variables
    public Vector3 Movement { get; private set; }

    // Events
    public delegate void Defeated(Robot_Controller robot);
    public static event Defeated OnDefeated;

    #region Unity methods

    private void OnEnable()
    {
        GameplayManager.OnGamePaused += EnterPause;
    }

    private void OnDisable()
    {
        GameplayManager.OnGamePaused -= EnterPause;
    }

    private void Awake()
    {
        // Set references
        rb = GetComponent<Rigidbody>();

        // State machine
        RobotStateMachine = new Robot_StateMachine();
        RobotIdleState = new Robot_IdleState(this, RobotStateMachine, robotData);
        RobotWalkState = new Robot_WalkState(this, RobotStateMachine, robotData, animator, walkAudioSource);
        RobotAttackState = new Robot_AttackState(this, RobotStateMachine, robotData, animator, attackAudioSource);
        RobotDefenseState = new Robot_DefenseState(this, RobotStateMachine, robotData, animator);

        RobotStateMachine.Initialize(RobotIdleState);

        IsDefending = false;
    }

    private void Start()
    {
        ResetRobot();

        WinCount = 0;
    }

    void Update()
    {
        RobotStateMachine.CurrentState.LogicUpdate();

        if (!GameplayManager.IsPaused)
        {
            // Calculate movement vector 
            CalculateMovement();
        }
    }

    private void FixedUpdate()
    {
        RobotStateMachine.CurrentState.PhysicsUpdate();

        // Move character
        MoveCharacter(Movement);
    }

    #endregion

    #region Methods

    private void CalculateMovement()
    {
        Movement = new Vector3(0, 0, MoveInput).normalized;
        moveDirection = Math.Sign(MoveInput);
    }

    private void MoveCharacter(Vector3 direction)
    {
        float currSpeed = robotData.movementSpeed * Time.fixedDeltaTime;
        // We multiply the 'speed' variable to the Rigidbody's velocity...
        // and also multiply 'Time.fixedDeltaTime' to keep the movement consistant on all devices
        rb.velocity = - direction * currSpeed;

        animator.SetFloat("speed", Mathf.Abs(MoveInput));
        animator.SetFloat("direction", MoveInput * -1);

    }

    /// <summary>
    /// Auxiliar method to stop the robot movement when game is paused
    /// </summary>
    private void EnterPause()
    {
        // Reset move input to 0 and movement to (0,0,0)
        MoveInput = 0;
        Movement = Vector3.zero;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Method to receive damage from any source
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage)
    {
        float damageToApply;

        if (IsDefending)
        {
            damageToApply = damage * (1f - robotData.armor);
        }
        else
        {
            damageToApply = damage;
        }

        ReproduceSound(takeDamageAudioSource, robotData.takeDamageSound, false);

        Health = Mathf.Max(0, Health - damageToApply);
        playerHUD.SetHealthBarValue(Health);

        if (Health <= 0)
        {
            IsDefeated = true;

            // Player defeated event
            if (OnDefeated != null)
            {
                OnDefeated(this);
            }
        }
    }

    /// <summary>
    /// Method to be called from the animator in the exact frame of the attack animation.
    /// </summary>
    public void AttackTrigger()
    {
        // Check targets on range
       Collider[] colliders = Physics.OverlapSphere(transform.position, robotData.attackRange);

        // If there are targets in range try to get IDamageable interface
        foreach (var collider in colliders)
        {
            // If collider has IDamageable interface
            if (collider.gameObject != gameObject)
            {
                // If collider is not the object try to get IDamageable
                var damageable = collider.GetComponent<IDamageable>();

                // Apply damage to the target
                if (damageable != null)
                {
                    var controller = collider.GetComponent<Robot_Controller>();

                    if (controller != null && controller.IsDefending)
                    {
                        controller.ReproduceSound(takeDamageAudioSource, robotData.impactDefendedSound, false);

                    }
                    else
                    {
                        controller.ReproduceSound(takeDamageAudioSource, robotData.impactNotDefendedSound, false);
                    }

                    // Create impact effect
                    CreateEffect(robotData.impactEffect, impactEffectTransform);

                    // Apply damage to target
                    damageable.ApplyDamage(robotData.attackDamage);

                    // Start camera shake
                    cameraControl.StartCameraShake();
                }
            }
        }
    }

    public void ResetRobot()
    {
        IsDefeated = false;

        Health = robotData.maxHealth;
        playerHUD.SetMaxHealthValue(robotData.maxHealth);
        playerHUD.SetHealthBarValue(Health);
        playerHUD.SetPlayerName(robotData.playerName);

        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }

    #endregion

    // 0 = both the numbers are equal
    // 1 = second number is smaller
    // -1 = first number is smaller
    public int CompareTo(Robot_Controller other)
    {
        if (null == other)
            return 1;
        int result = other.Health.CompareTo(this.Health);

        // string.Compare is safe when Id is null 
        return result;
    }

    public IEnumerator WaitAnimationToFinish(Robot_State nextState)
    {
        // Wait for the next frame to get the animator info (to wait the next animator state to load)
        yield return null;

        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorStateInfo(0).length);

        RobotStateMachine.ChangeState(nextState);
    }

    public void ReproduceSound(AudioSource audioSource, AudioClip audioClip, bool isLoop)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.Play();
    }

    public void CreateEffect(ParticleSystem particleSystem, Transform transform)
    {
        var effect = Instantiate(particleSystem, transform.position, transform.rotation, null);

        StartCoroutine(DestroyObjectOnTime(effect, effect.main.duration));
    }

    private IEnumerator DestroyObjectOnTime(UnityEngine.Object obj, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        Destroy(obj);
    }
}
