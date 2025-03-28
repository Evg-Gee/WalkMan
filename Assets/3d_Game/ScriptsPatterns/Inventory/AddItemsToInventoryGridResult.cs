namespace Inventory
{
    public readonly struct  AddItemsToInventoryGridResult
    {
        public readonly string inventoryOwnerId;
        public readonly int itemsToAddAmount;
        public readonly int itemsAddedAmount;

        public int ItemsNotAddedAmount => itemsToAddAmount - itemsAddedAmount;      // Возвращает количество которое мы пытаемся добавить минул количество которое добавлено
        
        public AddItemsToInventoryGridResult(
            string inventoryOwnerId,
            int itemsToAddAmount,
            int itemsAddedAmount
            )
        {
            this.inventoryOwnerId = inventoryOwnerId;
            this.itemsToAddAmount = itemsToAddAmount;
            this.itemsAddedAmount = itemsAddedAmount;
        }
    }
}