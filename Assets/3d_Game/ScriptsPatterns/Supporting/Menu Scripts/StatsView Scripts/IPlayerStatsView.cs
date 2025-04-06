public interface IPlayerStatsView
{
    void Initialize(IUserScoreInfo userScoreInfo);
    void UpdateHealth(int currentHealth);
    void AddHeart();
    void AddMonsterKill();
}