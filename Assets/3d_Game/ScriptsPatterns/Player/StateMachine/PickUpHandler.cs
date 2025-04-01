using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PickUpHandler : MonoBehaviour, IPickUpHandler
{
    private IInteractable _currentItem;
    public IInteractable CurrentItem => _currentItem;

    private CharPlayer _player;
    
    private Color _originalButtonColor;
    private Vector3 _originalButtonScale;
    private Sequence buttonSequence;

    private void Awake()
    {
        _player = GetComponentInParent<CharPlayer>();
        if (_player == null)
        {
            Debug.LogError("PickUpHandlerlayer должен быть прикреплён к объекту с CharPlayer");
            return;
        }
        var buttonImage = _player.HandButton.GetComponent<Image>();
        _originalButtonColor = buttonImage.color;
        _originalButtonScale = _player.HandButton.transform.localScale;
    }

    public void SetInteractableItem(IInteractable item) => _currentItem = item;


    public void RemoveInteractableItem(IInteractable item)
    {
        if (_currentItem == item)
            _currentItem = null;
    }
    public void PickUpItem()
    {
        if (_currentItem == null || _player == null) 
        {    
            return;
        }
        var state = new PickUpItemState(
            _player, 
            _player.StateMachine, 
            _currentItem, 
            _player.AnimController
        );
        
        _player.StateMachine.ChangeState(state);
        _currentItem = null; // Переносим очистку после создания состояния
        HidePickupButton();
    }     

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var item) && _currentItem == null)
        {
            SetInteractableItem(item);
            ShowPickupButton();
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out var item) && item == _currentItem)
        {
            RemoveInteractableItem(item);
            HidePickupButton();
        }
    }
    private void ShowPickupButton()
    {
        buttonSequence = DOTween.Sequence()
        
        .Append(_player.HandButton.transform.DOScale(1.2f, 0.5f).SetLoops(2, LoopType.Yoyo)) // Пульсация
        .Join(_player.HandButton.GetComponent<Image>().DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo)) // Мигание
        .SetLoops(int.MaxValue, LoopType.Restart)
        .Play(); // Мигание
        
        Debug.Log("ShowPickupButton");
    }

    private void HidePickupButton()
    {
        
         buttonSequence.Pause();
         _player.HandButton.transform.localScale = _originalButtonScale;
         _player.HandButton.GetComponent<Image>().color = _originalButtonColor;
         
         Debug.Log("HidePickupButton");
    }
}
