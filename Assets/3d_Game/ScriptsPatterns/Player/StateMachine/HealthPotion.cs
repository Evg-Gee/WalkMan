using UnityEngine;

public class HealthPotion : MonoBehaviour, IInteractable
{
    [SerializeField] private int healAmount;

    public void Interact(ICharacter character)
    {
        Debug.Log("charactermaxHealth + " + character.Stats.maxHealth);
        Destroy(gameObject.gameObject);
    }
}
