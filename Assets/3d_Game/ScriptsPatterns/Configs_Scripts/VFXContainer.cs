using UnityEngine;

[CreateAssetMenu(fileName = "PlayerVFX", menuName = "Configs/PlayerVFX")]
public class VFXContainer : ScriptableObject
{
    public TakeDamageFX _DamagePostProcessingFX;
    public ParticleSystem _meleeDamageFX;
    public GameObject _runingFX;  
    public GameObject _jumpingEndFX;
    public GameObject _stunGunIdleSprite;
    public ParticleSystem _shootingGunFX;
    public ParticleSystem _stunGunIdleFX;
}