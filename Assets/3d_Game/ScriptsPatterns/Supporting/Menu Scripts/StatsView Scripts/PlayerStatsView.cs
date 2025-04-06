using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsView : MonoBehaviour, IPlayerStatsView
{
    [Header("UI Images")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image heartIcon;
    [SerializeField] private Image monsterKillIcon1;
    [SerializeField] private Image monsterKillIcon2;

    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI heartText;
    [SerializeField] private TextMeshProUGUI monsterKillText;

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 0.5f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem heartEffect;
    [SerializeField] private ParticleSystem monsterKillEffect;

    private const float MaxHealth = 10f;
    private IUserScoreInfo _userScoreInfo;

    public void Initialize(IUserScoreInfo userScoreInfo)
    {
        _userScoreInfo = userScoreInfo;
        UpdateHeartText(_userScoreInfo.GetBloodHearts());
        UpdateMonsterKillText(_userScoreInfo.GetMonsterKills());
    }

    public void UpdateHealth(int currentHealth)
    {
        healthBar.fillAmount = currentHealth / MaxHealth;
    }

    public void AddHeart()
    {
        UpdateHeartText(_userScoreInfo.AddBloodHearts(1));
        PlayEffect(heartEffect);
        AnimateIcon(heartIcon);
    }

    public void AddMonsterKill()
    {
        UpdateMonsterKillText(_userScoreInfo.AddMonsterKills(1));
        PlayEffect(monsterKillEffect);
        AnimateMonsterIcons();
    }

    private void UpdateHeartText(int value)
    {
        heartText.text = value.ToString();
    }

    private void UpdateMonsterKillText(int value)
    {
        monsterKillText.text = value.ToString();
    }

    private void PlayEffect(ParticleSystem effect)
    {
        if (!effect.isPlaying)
        {
            effect.Play();
        }
    }

    private void AnimateIcon(Image icon)
    {
        icon.transform.localScale = Vector3.zero;
        icon.transform.DOScale(1f, animationDuration).SetEase(Ease.OutBounce);
    }

    private void AnimateMonsterIcons()
    {
        monsterKillIcon1.transform.DOShakePosition(0.85f, 15f).SetEase(Ease.InOutBack);
        monsterKillIcon2.transform.DOShakePosition(0.95f, 12.5f).SetEase(Ease.InOutBack);
    }
}
