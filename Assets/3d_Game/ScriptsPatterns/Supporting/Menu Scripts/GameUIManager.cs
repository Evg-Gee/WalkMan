using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsView _statsView;
    [SerializeField] private UserScoreInfo _scoreInfo;
    [SerializeField] private CharPlayer _player;

    private PlayerStatsPresenter _presenter;

    private void Start()
    {
        _presenter = new PlayerStatsPresenter(_statsView, _scoreInfo, _player);
        _player.InjectPresenter(_presenter); // можно передавать ссылку на презентер
    }

    public PlayerStatsPresenter Presenter => _presenter;
}