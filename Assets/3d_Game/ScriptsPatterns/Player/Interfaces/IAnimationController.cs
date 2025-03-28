public interface IAnimationController
{
    void SetRunning(bool isRunning);
    void SetTurn(float turnDirection);
    void ResetTurns();
}