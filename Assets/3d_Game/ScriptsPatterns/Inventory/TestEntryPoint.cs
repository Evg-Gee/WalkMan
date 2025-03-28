using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class TestEntryPoint : MonoBehaviour
    {
        [SerializeField] private ScreenView screenView;            //Чемодан, НПС, что угодно. Вьюху можно создавать, не обязательно SerializeField
        
        private InventoryService _inventoryService;
        private EquipmentController _equipmentController;

        private const string QWNER_1 = "EgGurDev_Equipment";
        private const string QWNER_2 = "Shop_For_Equipment";

        private readonly string[] _itemsIds = { "Gold_Pistol", "REd_Vintovka", "Silver_Heart", "Retro_Skin"};

        private string _cachedOwnerId;

        private void Start()
        {
            //Загружаем Конфиг далее
            //Всё должно быть асинхронно и при загрузочном экране ждать пока всё ниже загрузиться и так далее ниже
            var gameStateProvider = new GameStatePlayrPrefsProvider();

            gameStateProvider.LoadGameState();

            _inventoryService = new InventoryService(gameStateProvider);

            var gameState = gameStateProvider.GameState;
            foreach (var inventoryData in gameState.inventories)
            {
                _inventoryService.RegisterInventory(inventoryData);
            }

            _equipmentController = new EquipmentController(_inventoryService, screenView);
            _equipmentController.OpenInventory(QWNER_1);
            _cachedOwnerId = QWNER_1;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                _equipmentController.OpenInventory(QWNER_1);
                _cachedOwnerId = QWNER_1;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                _equipmentController.OpenInventory(QWNER_2);
                _cachedOwnerId = QWNER_2;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                var rIndex = Random.Range(0, _itemsIds.Length);
                var rItemId = _itemsIds[rIndex];
                var rAmount = Random.Range(1, 50);
                var result = _inventoryService.RemoveItems(_cachedOwnerId, rItemId, rAmount);

                Debug.Log($"Item Remove: {rItemId}. Trying to Remove: {result.itemsToRemoveAmount}, Success: {result.success}");
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                var rIndex = Random.Range(0, _itemsIds.Length);
                var rItemId = _itemsIds[rIndex];
                var rAmount = Random.Range(1, 50);
                var result = _inventoryService.AddItemsToInventory(_cachedOwnerId, rItemId, rAmount);

                Debug.Log($"Item added: {rItemId}. Amount added: {result.itemsToAddAmount}");
            }
        }
    }
}