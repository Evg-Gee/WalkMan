using UnityEditor;
using UnityEngine;

namespace GoogleSheetsImporter
{
    public class ConfigImportsMenu
    {
        private const string SPREADSHEET_ID = "1lK-mEXiCQk_c3nB_Im44onbIdCVU__XXuutkvkm8Kiw";
        private const string ITEMS_SHEETS_NAME = "EgGurDev_Equipment";
        private const string CREDENTIALS_PATH = "rezevel-4526dab20975.json";
        private const string SETTINGS_FILE_NAME = "rezevel-gameSettings.json";               //загрузка из ресурсов, файл дожен находиться в ресурсах

       [MenuItem("ResEvl2DSurvivors/Import Items Settings")]
        private static async void LoadItemsSettings()
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            var gameSettings = LoadSettings();                                               //var gameSettings = new GameSettings();

            var itemsParser = new ItemSettingsParser(gameSettings);
            await sheetsImporter.DownloadAndParseSheet(ITEMS_SHEETS_NAME, itemsParser);

            var jsonForSaving =JsonUtility.ToJson(gameSettings);
            PlayerPrefs.SetString(SETTINGS_FILE_NAME, jsonForSaving);           
            Debug.Log(jsonForSaving);      
        }

        private static GameSettings LoadSettings()
        {
            var jsonLoaded = PlayerPrefs.GetString(SETTINGS_FILE_NAME);                         //Загрузка из файла (game Settings) json или бинарный. имитация загрузки из файла
            var gameSettings = !string.IsNullOrEmpty(jsonLoaded)
                ? JsonUtility.FromJson<GameSettings>(jsonLoaded)
                : new GameSettings();

            return gameSettings;
        }
    }
}