using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class CharPlayer : MonoBehaviour, ICharacter
{
    [SerializeField] private PlayerStatsSO _stats;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private FloatingJoysticks _movementJoystick;
    [SerializeField] private FloatingJoysticks _lookJoystick;
    
    private IJoystick _moveInput;
    private IJoystick _lookInput;
    
    private StateMachine _stateMachine;
    private IInputHandler _input;
    private IAnimationController animController;
    private IRotationHandler rotationHandler;
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public PlayerStatsSO Stats => _stats;
    public IInputHandler InputHandler => _input;
    public Transform CharacterTransform => transform;
    public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        rotationHandler = new RotationHandler();
        animController = new AnimationController(Animator);
        _moveInput = new FloatingJoystickWrapper(_movementJoystick);
        _lookInput = new FloatingJoystickWrapper(_lookJoystick);
        _input = new CharInput(_moveInput, _lookInput);
        
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(this, _stateMachine,rotationHandler));
        _stateMachine.AddState(new RunningState(this, _stateMachine, animController, rotationHandler));
        _stateMachine.AddState(new JumpingState(this, _stateMachine));
        _stateMachine.AddState(new AttackState(this, _stateMachine));
        _stateMachine.ChangeState<IdleState>();
    }

    private void Update()
    {
        UpdateGroundStatus();
        _stateMachine.CurrentState?.HandleInput();
        _stateMachine.CurrentState?.Update();
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
}