using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;
        public event Action<Sprite> ItemSpriteChanged;

        private readonly InventorySlotData _data;

        public string ItemId 
        { 
            get => _data.itemId; 
            set
            {
                if (_data.itemId != value)
                {
                    _data.itemId = value;
                    ItemIdChanged?.Invoke(value);
                }
            }
        }
        public int Amount 
        { 
            get => _data.amount;
            set
            {
                if (_data.amount != value)
                {
                    _data.amount = value;
                    ItemAmountChanged?.Invoke(value);
                }
            }
        }
        public Sprite ItemSprite
        {
            get => _data.itemSprite;
            set
            {
                if (_data.itemSprite != value)
                {
                    _data.itemSprite = value;
                    ItemSpriteChanged?.Invoke(value);   
                }
            }
        }

        public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

        public InventorySlot(InventorySlotData data) 
        {
            _data = data;
        }
    }
}