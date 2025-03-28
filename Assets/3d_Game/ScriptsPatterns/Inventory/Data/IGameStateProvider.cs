namespace Inventory
{
    public interface IGameStateProvider
    {
        //сделать не через void, а через async
        void SaveGameState();
        void LoadGameState();
    }
}