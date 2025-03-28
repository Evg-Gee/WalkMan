using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public Sprite itemSprite;
        public string itemId;
        public int amount;       
    }
}