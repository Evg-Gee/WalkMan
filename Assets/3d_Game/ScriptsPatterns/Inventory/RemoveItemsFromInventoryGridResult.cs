namespace Inventory
{
    public readonly struct RemoveItemsFromInventoryGridResult
    {
        public readonly string inventoryOwnerId;
        public readonly int itemsToRemoveAmount;
        public readonly bool success;

        public RemoveItemsFromInventoryGridResult(
            string inventoryOwnerId,
            int itemsToRemoveAmount,
            bool success
            )
        {
            this.inventoryOwnerId = inventoryOwnerId;
            this.itemsToRemoveAmount = itemsToRemoveAmount;
            this.success = success;
        }
    }
}