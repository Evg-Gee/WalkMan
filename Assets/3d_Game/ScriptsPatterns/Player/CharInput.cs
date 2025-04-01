using UnityEngine;
using UnityEngine.InputSystem;
using Player;
public class CharInput : IInputHandler
{    
    private readonly Player3dControls _controls;
    private readonly IJoystick _moveJoystick;
    private readonly IJoystick _lookJoystick;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool PickUp { get; private set; }
        
    public CharInput(IJoystick moveJoystick, IJoystick lookJoystick) {
        _moveJoystick = moveJoystick;
        _lookJoystick = lookJoystick;
        _controls = new Player3dControls();
        ConfigureControls();
        _controls.Enable();
    }
    private void ConfigureControls()
    {
        _controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
        _controls.Player.Jump.performed += ctx => JumpPressed = true;
        _controls.Player.PickUp.performed += ctx => PickUp = true;
        _controls.Player.MeleeAttack.performed += ctx => AttackPressed = true;
        _controls.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        _controls.Player.Look.canceled += ctx => LookInput = Vector2.zero;
    }
    public void Enable() => _controls.Player.Enable();
    public void Disable() => _controls.Player.Disable();
    public void ResetActions()
    {
        JumpPressed = false;
        AttackPressed = false;
        PickUp = false;
    }
        
        
}
