using UnityEngine;

namespace Inventory
{
    public class InventoryGridView : MonoBehaviour
    {
        private IReadOnlyInventoryGrid _inventory;
        public void Setup(IReadOnlyInventoryGrid inventory)
        {
            _inventory = inventory;
            PrintInventori();
        }

        public void PrintInventori()
        {
            var slots = _inventory.GetSlots();
            var size = _inventory.Size;
            var result = "";
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var slot = slots[i, j];
                    result += $"Slot ({i}:{j}). Item: {slot.ItemId}, {slot.Amount}";
                }
            }

            Debug.Log(result);
        }
    }
}