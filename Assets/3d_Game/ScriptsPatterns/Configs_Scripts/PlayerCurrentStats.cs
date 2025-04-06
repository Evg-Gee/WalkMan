using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCurrentStats", menuName = "Game/PlayerCurrentStats", order = 1)]
public class PlayerCurrentStats : ScriptableObject
{
    public PlayerStatsData statsData;
}
