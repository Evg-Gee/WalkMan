using System.IO;
using UnityEngine;

public class UserScoreInfo : MonoBehaviour, IUserScoreInfo
{
    [SerializeField] private string _fileName = "UserScoreData.json";
    private UserScoreData _data = new UserScoreData();
    private string FilePath => Path.Combine(Application.persistentDataPath, _fileName);
    public PlayerStatsData playerStatsData;
    public PlayerStatsSO  playerStatsSO;
    private void Awake()
    {
        LoadUserData();
    }

    private void OnEnable()
    {
        LoadUserData();
    }

    private void OnDisable()
    {
        SaveUserData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveUserData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveUserData();
    }

    public int AddBloodHearts(int amount)
    {
        _data.bloodHearts += amount;
        return _data.bloodHearts;
    }

    public int AddMonsterKills(int amount)
    {
        _data.monstersKilled += amount;
        return _data.monstersKilled;
    }

    public int GetBloodHearts() => _data.bloodHearts;
    public int GetSilverHearts() => _data.silverHearts;
    public int GetGoldHearts() => _data.goldHearts;
    public int GetMonsterKills() => _data.monstersKilled;

    public void SaveUserData()
    {
        try
        {
            // Сохраняем данные о здоровье
            _data.currentHealth = playerStatsData.currentHealth;
            _data.maxHealth = playerStatsSO.maxHealth;

            // Сохраняем остальные данные
            _data.bloodHearts = playerStatsData.bloodHearts;
            

            // Сохраняем все данные в файл
            string json = JsonUtility.ToJson(_data, true);
            File.WriteAllText(FilePath, json);
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save user data: {e.Message}");
        }
    }

    private void LoadUserData()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                _data = JsonUtility.FromJson<UserScoreData>(json);

                // Теперь мы загружаем здоровье из сохранённых данных
                playerStatsData.currentHealth = _data.currentHealth;
                playerStatsSO.maxHealth = _data.maxHealth;
            }
            else
            {
                _data = new UserScoreData(); // если нет сохранённых данных
                playerStatsData = new PlayerStatsData(); // устанавливаем значения по умолчанию
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to load user data: {e.Message}");
            _data = new UserScoreData(); // обрабатываем ошибку и устанавливаем значения по умолчанию
            playerStatsData = new PlayerStatsData();
        }
    }

}
