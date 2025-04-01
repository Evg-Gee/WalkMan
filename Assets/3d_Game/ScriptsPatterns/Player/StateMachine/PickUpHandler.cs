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
        var buttonImage = _player.HandButton.image;
        _originalButtonColor = buttonImage.color;
        _originalButtonScale = _player.HandButton.transform.localScale;
        
        HidePickupButton();
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
        _player.StateMachine.AddState(state);
        _player.StateMachine.ChangeState(state);
        _currentItem = null; 
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
        _player.HandButton.gameObject.SetActive(true);
        buttonSequence = DOTween.Sequence()
        
        .Append(_player.HandButton.transform.DOScale(1.2f, 0.5f).SetLoops(2, LoopType.Yoyo)) 
        .Join(_player.HandButton.image.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo)) 
        .SetLoops(int.MaxValue, LoopType.Restart)
        .Play(); 
    }

    private void HidePickupButton()
    {
        
        buttonSequence.Pause();
        _player.HandButton.transform.localScale = _originalButtonScale;
        _player.HandButton.image.color = _originalButtonColor;
        _player.HandButton.gameObject.SetActive(false);
    }
}
