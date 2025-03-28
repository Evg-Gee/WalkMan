using System;
using UnityEngine;

namespace Inventory
{
    public interface IReadOnlyInventorySlot   // Передаём его во вьюху(в представление) и от туда мы хотим знать когда данные изменились
    {
        event Action<string> ItemIdChanged;
        event Action<int> ItemAmountChanged;
        event Action<Sprite> ItemSpriteChanged;

        string ItemId {  get;}
        int Amount { get;}
        Sprite ItemSprite { get; }

        bool IsEmpty { get;}
    }
}