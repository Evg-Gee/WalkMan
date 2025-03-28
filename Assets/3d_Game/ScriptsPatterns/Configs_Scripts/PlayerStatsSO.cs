using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Configs/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Numeric Configs")]
    [SerializeField] private float speed = 14f;
    public float jumpForce = 36f;
    public int maxHealth = 15;
    public int meleeDamage = 4;
    public float meleeCooldown = 0.5f;
    public int rangedDamage = 2;
    public float rangedCooldown = 0.35f;
    public int bulletAmount = 20;
    public float meleeStunDamage = 4f;
    public float meleeStunDelay = 1f;
    public float groundCheckRadius = 0.1f;
    public float groundUpCheckRadius = 0.42f;
    public float meleeCheckRadius = 2.64f;
    [Space (5f)]
    [Header("Rotation Camera")]
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _accelerationSpeed = 8f;
    [SerializeField] private float _decelerationSpeed = 30f;
    [SerializeField] private float _turnAngleThreshold = 10f;
    public float Acceleration => _accelerationSpeed;
    public float Deceleration => _decelerationSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float Speed => speed;
    public float TurnAngleThreshold =>_turnAngleThreshold;
    
    [Space (5f)]
    [Header("Wisp Configs")]
    public WispBullet wispPrefab;
    [Space (10f)]
    [Header("LayerMask")]
    public LayerMask EnemyLayer;
    public LayerMask GroundLayer;
    public LayerMask BonusLayer;
}