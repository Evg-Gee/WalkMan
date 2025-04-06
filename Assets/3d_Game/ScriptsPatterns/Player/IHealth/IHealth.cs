public interface IHealth
{
    int Current { get; }
    int Max { get; }
    void Heal(int amount);
}
