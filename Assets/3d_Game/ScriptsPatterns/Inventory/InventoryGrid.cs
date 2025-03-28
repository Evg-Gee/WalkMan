using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        public event Action<Vector2Int> SizeChanged;
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;

        public Vector2Int Size
        {
            get => _data.sizeInventory;
            set
            {
                if (_data.sizeInventory != value)
                {
                    _data.sizeInventory = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }
        public string OwnerId => _data.ownerId;

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new ();                // Координата яцейки и сама ячейка

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;
            var size = _data.sizeInventory;
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var index = i*size.y + j;                                               // перевод двумерного массива в однамерный
                    var slotData = data.inventorySlots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                }
            }
        }

        public AddItemsToInventoryGridResult AddItems(string itemId, int amount = 1)                                       //Добавление предмета в инвентарь
        {
            var remainingAmount = amount;
            var itemsAddedToSlotWithSameItemsAmount = AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
            {
                return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedToSlotWithSameItemsAmount);
            }

            var itemsAddedToAvailableSlotsAmount = AddToFirstAvailableSlots(itemId, remainingAmount, out remainingAmount);
            var totalAddedItemsAmount = itemsAddedToSlotWithSameItemsAmount + itemsAddedToAvailableSlotsAmount;

            return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedItemsAmount);
        }

        public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, string itemID, int amount = 1)                //Добавление предмета в конкретную ячейку
        {
            var slot = _slotsMap[slotCoords];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
            {
                slot.ItemId = itemID;
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemID);

            if(newValue > itemSlotCapacity)                                     // Если текущее значение НЕ влазиет в слот и остаток невлезавшего переносим в пустую ячейку
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemID, remainingItems);

                itemsAddedAmount += result.itemsAddedAmount;
            }
            else                                                                // Если текущее значение влазиет в слот
            {
                itemsAddedAmount = amount;
                slot.Amount =newValue;                                          // В модель записываем нужную сумму
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }
        public RemoveItemsFromInventoryGridResult RemoveItems(string itemID, int amount = 1)                                    //Удаление предмета из инвентарь
        {          
             if(!Has(itemID, amount))
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

             var amountToRemove = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoords];

                    if(slot.ItemId != itemID)
                    {
                        continue;
                    }

                    if(amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;

                        RemoveItems(slotCoords, itemID, slot.Amount);                        
                    }
                    else
                    {
                        RemoveItems(slotCoords, itemID, amountToRemove);
                        return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
                    }
                }
            }
            throw new Exception("Something went wrong");            
        }
        public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoords, string itemID, int amount = 1)             //Удаление предмета из конкретной ячейки
        {
            var slot = _slotsMap[slotCoords];

            if (slot.IsEmpty || slot.ItemId != itemID || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            slot.Amount -= amount;
            slot.ItemSprite = Resources.Load<Sprite>(itemID);

            if (slot.Amount == 0)
            {
                slot.ItemId = null;
                slot.ItemSprite = Resources.Load<Sprite>(slot.ItemId);
            }

            return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
        }

        public int GetAmount(string itemID)
        {
            var amount = 0;
            var slots = _data.inventorySlots;

            foreach (var slot in slots) 
            {
                if(slot.itemId == itemID)
                {
                    amount += slot.amount;
                }            
            }

            return amount;
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];
            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];

                }
            }
            return array;
        }

        public bool Has(string itemID, int amount)
        {
            var amountExist = GetAmount(itemID);

            return amountExist >= amount;
        }

        private int GetItemSlotCapacity(string itemID) // яасть 2 - 9,25 мин
        {
            return 99;
        }

        public void SwitchSlots(Vector2Int slotCoordsA, Vector2Int slotCoordsB)
        {
            var slotsA = _slotsMap[slotCoordsA];
            var slotsB = _slotsMap[slotCoordsB];
            var tempSlotItemId = slotsA.ItemId;
            var tempSlotItemAmount = slotsA.Amount;
            slotsA.ItemId = slotsB.ItemId;
            slotsA.Amount = slotsB.Amount;
            slotsB.ItemId = tempSlotItemId;
            slotsB.Amount = tempSlotItemAmount;
        }

        private int AddToSlotsWithSameItems(string itemID, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; (i < Size.x); i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                         continue;
                    }

                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                    if(slot.Amount >= slotItemCapacity)
                    {
                        continue;
                    }

                    if(slot.ItemId != itemID)
                    {
                        continue;
                    }

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;  
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                        slot.ItemSprite = Resources.Load<Sprite>(itemID);

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;
                        slot.ItemSprite = Resources.Load<Sprite>(itemID);

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int AddToFirstAvailableSlots(string itemID, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; (i < Size.x); i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int (i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.ItemId = itemID;
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);

                    if(newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemToAddAmount;
                        slot.ItemSprite = Resources.Load<Sprite>(itemID);

                        slot.Amount = slotItemCapacity;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;
                        slot.ItemSprite = Resources.Load<Sprite>(itemID);

                        return itemsAddedAmount;
                    }
                }
            }
            return itemsAddedAmount;
        }
    }
}