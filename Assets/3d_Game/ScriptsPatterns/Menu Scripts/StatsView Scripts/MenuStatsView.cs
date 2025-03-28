using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuStatsView : MonoBehaviour
{
    [Header("Text coomponents")]
    [SerializeField] private TextMeshProUGUI _scoreBloodHeartText;
    [SerializeField] private TextMeshProUGUI _scoreSilverHeartText;
    [SerializeField] private TextMeshProUGUI _scoreGoldHeartText;

    void Start()
    {
        ResetUIInfo();
    }

    private void ResetUIInfo()
    {
        _scoreBloodHeartText.text = UserScoreInfo.instance.GetBloodHeartInt().ToString();
        _scoreSilverHeartText.text = UserScoreInfo.instance.GetSilverHeartInt().ToString();
        _scoreGoldHeartText.text = UserScoreInfo.instance.GetGoldHeartInt().ToString();
    }
}
