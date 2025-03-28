public class FloatingJoystickWrapper : IJoystick {
    private readonly FloatingJoysticks _joystick;       
    
    public FloatingJoystickWrapper(FloatingJoysticks joystick) 
    {
        _joystick = joystick;
    }
    
    public void Enable() => _joystick.gameObject.SetActive(true);
    public void Disable() => _joystick.gameObject.SetActive(false);
}