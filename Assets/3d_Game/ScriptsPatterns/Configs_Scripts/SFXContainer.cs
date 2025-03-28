using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSFX", menuName = "Configs/PlayerSFX")]
public class SFXContainer : ScriptableObject
{
    public AudioClip jumpAFX;
    public AudioClip _jumpingEnd;
    public AudioClip _runing;
    public AudioClip _idleBreathing;
    public AudioClip _meleeAttackAFX;
    public AudioClip _meleeStunAttackAFX;
    public AudioClip _meleeDamageAFX;
    public AudioClip pistolShotAFX;
   
}