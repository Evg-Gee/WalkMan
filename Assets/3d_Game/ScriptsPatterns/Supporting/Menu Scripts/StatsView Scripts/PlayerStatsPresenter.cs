public class PlayerStatsPresenter
{
    private readonly IPlayerStatsView _view;
    private readonly IUserScoreInfo _score;
    private readonly IHealth _health;

    public PlayerStatsPresenter(IPlayerStatsView view, IUserScoreInfo scoreInfo, IHealth health)
    {
        _view = view;
        _score = scoreInfo;
        _health = health;

        _view.Initialize(scoreInfo);
        _view.UpdateHealth(_health.Current);        
    }


    public void OnHeartCollected()
    {
        _view.AddHeart();
    }

    public void OnMonsterKilled()
    {
        _view.AddMonsterKill();
    }

    public void Heal(int amount)
    {
        _health.Heal(amount);
        _view.UpdateHealth(_health.Current);
    }

    public void SaveProgress() => _score.SaveUserData();
}