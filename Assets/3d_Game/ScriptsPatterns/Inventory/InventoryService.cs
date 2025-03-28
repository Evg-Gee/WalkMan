using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryService
    {
        private readonly IGameStateSaver _gameStateSaver;
        private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new ();

        public InventoryService(IGameStateSaver gameStateSaver)
        {
            _gameStateSaver = gameStateSaver;
        }

        public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
        {
            var inventory = new InventoryGrid(inventoryData);
            _inventoriesMap[inventory.OwnerId] = inventory;

            // Загружаем спрайты для каждого слота
            foreach (var slotData in inventoryData.inventorySlots)
            {
                if (!string.IsNullOrEmpty(slotData.itemId))
                {
                    slotData.itemSprite = Resources.Load<Sprite>(slotData.itemId); // Загружаем спрайт по ID
                }
            }

            return inventory;
        }
        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.AddItems(itemId, amount);
            _gameStateSaver.SaveGameState();
            return result;
        }
        public AddItemsToInventoryGridResult AddItemsToInventory(
            string ownerId,
            Vector2Int slotCoords,
            string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.AddItems(slotCoords, itemId, amount);
            _gameStateSaver.SaveGameState();
            return result;
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.RemoveItems(itemId, amount);
            _gameStateSaver.SaveGameState();
            return result;
        }
        public RemoveItemsFromInventoryGridResult RemoveItemsFromSlot(
            string ownerId,
            Vector2Int slotCoords,
            string itemId, 
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.RemoveItems(slotCoords, itemId, amount);
            _gameStateSaver.SaveGameState();
            return result;
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }         

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }
    }
}