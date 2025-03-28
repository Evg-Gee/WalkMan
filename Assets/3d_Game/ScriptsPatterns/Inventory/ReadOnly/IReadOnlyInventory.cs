using System;

namespace Inventory
{
    public interface IReadOnlyInventory // В нем хотим получать события на получения, добавления или удаление какого либо предмета
    {
        event Action<string, int> ItemsAdded;
        event Action<string, int> ItemsRemoved;

        string OwnerId { get; }

        int GetAmount(string itemID);
        bool Has(string itemID, int amont);
    }
}