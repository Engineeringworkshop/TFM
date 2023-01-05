using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Controller : MonoBehaviour, IDamageable, IComparable<Robot_Controller>
{
    [Header("Stats")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] public string playerName;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage;
    [SerializeField] private Transform startPosition;

    [Header("Controls")]
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode attackKey;
    [SerializeField] private KeyCode defenseKey;

    [Header("References")]
    [SerializeField] public PlayerHUDController playerHUD;

    [Header("Debug")]
    [SerializeField] private float moveInput;

    // Properties
    public float Health { get; private set; }
    public bool IsDefeated { get; private set; }
    public int WinCount { get; set; }

    // Components
    private Rigidbody rb;

    // Variables
    private Vector3 movement;

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
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ResetRobot();

        WinCount = 0;
    }

    void Update()
    {
        if (!GameplayManager.IsPaused)
        {
            // Check player inputs
            CheckInputs();

            // Calculate movement vector 
            CalculateMovement();
        }
    }

    private void FixedUpdate()
    {
        // Move character
        MoveCharacter(movement);
    }

    #endregion

    #region Methods

    private void CheckInputs()
    {
        // Left movement control
        if (Input.GetKeyDown(moveLeftKey))
        {
            print("Left key pushed by " + name);

            moveInput = 1;
        }
        else if (Input.GetKeyUp(moveLeftKey))
        {
            print("Left key released by " + name);

            moveInput = 0;
        }

        // Right movement control
        if (Input.GetKeyDown(moveRightKey))
        {
            print("Right key pushed by " + name);

            moveInput = -1;
        }
        else if (Input.GetKeyUp(moveRightKey))
        {
            print("Right key released by " + name);

            moveInput = 0;
        }


        // Attack control
        if (Input.GetKeyDown(attackKey))
        {
            print("Attack key pushed by " + name);

            AttackTrigger();
        }

        // Defense control
        if (Input.GetKeyDown(defenseKey))
        {
            print("Defense key pushed  by " + name);
        }

        // Debug
        // print("Move input: " + moveInput);
    }

    private void CalculateMovement()
    {
        movement = new Vector3(0, 0, moveInput).normalized;
    }

    private void MoveCharacter(Vector3 direction)
    {
        // We multiply the 'speed' variable to the Rigidbody's velocity...
        // and also multiply 'Time.fixedDeltaTime' to keep the movement consistant on all devices
        rb.velocity = - direction * movementSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Auxiliar method to stop the robot movement when game is paused
    /// </summary>
    private void EnterPause()
    {
        // Reset move input to 0 and movement to (0,0,0)
        moveInput = 0;
        movement = Vector3.zero;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Method to receive damage from any source
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage)
    {
        Health = Mathf.Max(0, Health - damage);
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
       Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        // If there are targets in range try to get IDamageable interface
        foreach (var collider in colliders)
        {
            // If collider has IDamageable interface
            if (collider.gameObject != gameObject)
            {
                // If collider is not the object try to get IDamageable
                var obj = collider.GetComponent<IDamageable>();

                // Apply damage to the target
                if (obj != null)
                {
                    obj.ApplyDamage(attackDamage);
                }
            }
        }
    }

    public void ResetRobot()
    {
        IsDefeated = false;

        Health = maxHealth;
        playerHUD.SetMaxHealthValue(maxHealth);
        playerHUD.SetHealthBarValue(Health);
        playerHUD.SetPlayerName(playerName);

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
}
