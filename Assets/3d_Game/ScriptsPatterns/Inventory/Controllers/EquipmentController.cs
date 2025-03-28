namespace Inventory
{
    public class EquipmentController
    {
        private readonly InventoryService _inventoryService;
        private readonly ScreenView _view;

        private InventoryGridController _currentInventoryController;

       public EquipmentController(InventoryService inventoryService, ScreenView view) 
        {
            _inventoryService = inventoryService;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var innvenory =_inventoryService.GetInventory(ownerId);
            var innvenoryView = _view.InventoryView;

            _currentInventoryController = new InventoryGridController(innvenory, innvenoryView);
        }
    }
}