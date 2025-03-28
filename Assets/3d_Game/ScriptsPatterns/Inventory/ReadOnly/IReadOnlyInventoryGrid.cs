using System;
using UnityEngine;

namespace Inventory
{
    public interface IReadOnlyInventoryGrid : IReadOnlyInventory // Сетка инвентиоря, Расширяет функционал IReadOnlyInventory
    {
        event Action<Vector2Int> SizeChanged;
        Vector2Int Size {  get; }

        IReadOnlyInventorySlot[,] GetSlots();
    }
}
