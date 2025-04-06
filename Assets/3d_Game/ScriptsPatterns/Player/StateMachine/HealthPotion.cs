using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private int healAmount;

    public void Interact(ICharacter character)
    {
        if (character is CharPlayer player)
        {
            player.Heal(healAmount);
            player.Presenter?.Heal(healAmount);  // Обновление UI

            // Сохраняем данные после изменения здоровья
            var userScoreInfo = FindObjectOfType<UserScoreInfo>();
            userScoreInfo.SaveUserData(); // сохраняем данные о здоровье
        }

        Destroy(gameObject, 0.75f);
    }
}
