using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class GameStatePlayrPrefsProvider : IGameStateProvider, IGameStateSaver
    {
        private const string KEY = "GAME STATE"; //Можно изменить на подключение к файлу или серверу

        public GameStateData GameState {  get; private set; }
        
        public void SaveGameState()
        {
            var json = JsonUtility.ToJson(GameState);
            PlayerPrefs.SetString(KEY, json);
        }
        public void LoadGameState()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var json = PlayerPrefs.GetString(KEY);
                GameState =JsonUtility.FromJson<GameStateData>(json);
            }
            else
            {
                GameState = InitFromSettings();
                SaveGameState();
            }
        }

        private GameStateData InitFromSettings()                                   //Имитация загрузки кофигов
        {
            var gameState = new GameStateData
            {
                inventories = new List<InventoryGridData>
                 {
                     CreateTestInventory("EgGurDev_Equipment"),                    //Вместо строки должны отправляться конфиги и заполняться в методе CreateTestInventory
                     CreateTestInventory("Shop_For_Equipment")
                 }
            };

            return gameState;
        }

        private InventoryGridData CreateTestInventory(string ownerId)
        {
            var size = new Vector2Int(3, 4);
            var createTestInventorySlot = new List<InventorySlotData>();
            var length = size.x * size.y;                                //size брать из конфигов
            for (var i = 0; i < length; i++)
            {
                createTestInventorySlot.Add(new InventorySlotData());
            }
            var createTestInventoryData = new InventoryGridData
            {
                ownerId = ownerId,
                sizeInventory = size,

                inventorySlots = createTestInventorySlot
            };

            return createTestInventoryData;
        }
    }
}