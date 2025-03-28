using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsView : MonoBehaviour
{
    public static PlayerStatsView instance;
    [Header("Image coomponents")]
    [SerializeField] private Image _hpSlider;
    [SerializeField] private Image _heartBlooodImage;
    [SerializeField] private Image _monstersKillImage;
    [SerializeField] private Image _monstersKillImage2;
    [Header("Text coomponents")]
    [SerializeField] private TextMeshProUGUI _scoreBloodHeartText;
    [SerializeField] private TextMeshProUGUI _scoreMonstersKillText;
    [Header("Numeric vaules")]
    [SerializeField] private float _durationTime;
    [Header("Particle System FX")]
    [SerializeField] private ParticleSystem _heartFX;
    [SerializeField] private ParticleSystem _monstersKillFX;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ResetUIInfo();
    }
    public void HeartUIUpdate()
    {
        HeartUIText();
        if (!_heartFX.isPlaying) 
        {
            _heartFX.Play();
        }
        else
        {
            Debug.Log("scoreHeartFX Is Play();");
        }
        _heartBlooodImage.transform.localScale = Vector3.zero;
        _heartBlooodImage.transform.DOScale(1f, _durationTime).SetEase(Ease.OutBounce)
        .Play();
    }

    public void MonstersKillUIUpdate()
    {
        MonstersKillUIText();
        if (!_monstersKillFX.isPlaying)
        {
            _monstersKillFX.Play();
        }
        else
        {
            Debug.Log("monstersKillFX Is Play();");
        }
        _monstersKillImage.transform.DOShakePosition(0.85f, 15f).SetEase(Ease.InOutBack)
        .Play();
        _monstersKillImage2.transform.DOShakePosition(0.95f, 12.5f).SetEase(Ease.InOutBack)
        .Play();
    }

    private void HeartUIText( )
    {
        _scoreBloodHeartText.text = UserScoreInfo.instance.PlusScoreHeart(1).ToString(); ;
    }
    private void MonstersKillUIText()
    {
        _scoreMonstersKillText.text = UserScoreInfo.instance.SetMonstersKill(1).ToString();
    }
    public void UpdateView(int value)
    {
        _hpSlider.fillAmount = value/10f;
    }
    private void ResetUIInfo()
    {
        _scoreBloodHeartText.text = UserScoreInfo.instance.GetBloodHeartInt().ToString(); 
        _scoreMonstersKillText.text = UserScoreInfo.instance.GetMonstersKill().ToString();
    }  
}