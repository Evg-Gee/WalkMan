using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class CharPlayer : MonoBehaviour, ICharacter, IHealth
{
    [SerializeField] private PlayerCurrentStats playerCurrentStats;
    [SerializeField] private PlayerStatsSO _stats;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private FloatingJoysticks _movementJoystick;
    [SerializeField] private FloatingJoysticks _lookJoystick;
    [SerializeField] private Button _handButton;
   
    public int maxHealth { get; private set; }
    private int currentHealth;

    public int Current => currentHealth;
    public int Max => maxHealth;
    public PlayerStatsPresenter Presenter { get; private set; }
    
    private IJoystick _moveInput;
    private IJoystick _lookInput;
    
    private StateMachine _stateMachine;
    private IInputHandler _input;
    private IAnimationController _animController;
    private IRotationHandler rotationHandler;
    private IMovementHandler movementHandler;
    private PickUpHandler _pickUpHandler;
    
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public PlayerStatsSO Stats => _stats;
    public Button HandButton => _handButton;
    public IInputHandler InputHandler => _input;
    public Transform CharacterTransform => transform;
    public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }
    public bool IsGrounded { get; private set; }
    
    public StateMachine StateMachine => _stateMachine;
    public IAnimationController AnimController => _animController;

    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        rotationHandler = new RotationHandler(Stats.RotationSpeed);
        movementHandler = new MovementHandler(Stats.Speed, Stats.Acceleration, Stats.Deceleration);
        _animController = new AnimationController(Animator);
        _moveInput = new FloatingJoystickWrapper(_movementJoystick);
        _lookInput = new FloatingJoystickWrapper(_lookJoystick);
        _input = new CharInput(_moveInput, _lookInput);        
        _pickUpHandler = GetComponentInChildren<PickUpHandler>();
        if (_pickUpHandler == null)
        {
            Debug.LogWarning("Компонент PickUpHandlerlayer не найден, добавляем динамически.");
        }
        
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(this, _stateMachine,rotationHandler));
        _stateMachine.AddState(new RunningState(this, _stateMachine, _animController, rotationHandler, movementHandler));
        _stateMachine.AddState(new JumpingState(this, _stateMachine, _animController));
        _stateMachine.AddState(new AttackState(this, _stateMachine));
        _stateMachine.AddState(new TakeDamageState(this, _stateMachine, _animController));             
        
        _stateMachine.ChangeState<IdleState>();
    }

    void Start()
    {
        if (playerCurrentStats != null)
        {
            // Загрузка данных о здоровье из сохранений или из ScriptableObject
            maxHealth = _stats.maxHealth;
            currentHealth = playerCurrentStats.statsData.currentHealth;
        }

        // Загрузим данные из UserScoreInfo, если они есть
        var userScoreInfo = FindObjectOfType<UserScoreInfo>();
        if (userScoreInfo != null)
        {
            currentHealth = userScoreInfo.playerStatsData.currentHealth;
            maxHealth = _stats.maxHealth;
        }
    }

    private void Update()
    {
        UpdateGroundStatus();
        _stateMachine.Update();
        
         
         if (_input.PickUpPressed && _pickUpHandler.CurrentItem != null)
        {
            _pickUpHandler?.PickUpItem();
        }
        
        _input.ResetActions();      
    }

    private void FixedUpdate() => _stateMachine.CurrentState?.PhysicsUpdate();

    public void UpdateGroundStatus()
    {
        IsGrounded = Physics.Raycast(
            transform.position, 
            Vector3.down, 
            _groundCheckDistance, 
            _groundLayer
        );
    }
    public void InjectPresenter(PlayerStatsPresenter presenter)
    {
        Presenter = presenter;
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}