using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryGridData
    {
        public string ownerId;
        public List<InventorySlotData> inventorySlots;
        public Vector2Int sizeInventory;
    }
}
