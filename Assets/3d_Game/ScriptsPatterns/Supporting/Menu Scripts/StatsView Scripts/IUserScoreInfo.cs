using UnityEngine;
using System.IO;

public interface IUserScoreInfo
{
    int AddBloodHearts(int amount);
    int AddMonsterKills(int amount);

    int GetBloodHearts();
    int GetSilverHearts();
    int GetGoldHearts();
    int GetMonsterKills();

    void SaveUserData();
}