using UnityEngine;

public class PickUpItemState : BaseState
{
    private readonly IAnimationController _animationController;
    private readonly IInteractable _item;
   private bool _animationCompleted;

    public PickUpItemState(ICharacter character, 
        StateMachine stateMachine, 
        IInteractable item,
        IAnimationController animationController)
        : base(character, stateMachine)
    {
        _item = item;
        _animationController = animationController;
    }

    public override void Enter()
    {   
        _animationCompleted = false;
        _animationController.OnAnimationComplete += HandleAnimationComplete;
        _animationController.SetState(AnimationState.Pickup);
        Debug.Log("Update PickUpItemState");
    }

    public override void Exit() 
    {
        _animationController.OnAnimationComplete -= HandleAnimationComplete;
        _animationController.SetState(AnimationState.Idle);
    } 

    public override void Update()
    {     
        if (_animationCompleted)
        {
            _item.Interact(Character);
            TryChangeState<IdleState>();
        }
    }
    private void HandleAnimationComplete(AnimationState completedState)
    {
        if (completedState == AnimationState.Pickup)
        {
            _animationCompleted = true;
        }
        else
        {
            _animationCompleted =false;
        }
    }
}
