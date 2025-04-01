using UnityEngine;

public class PickUpItemState : BaseState
{
    private readonly IAnimationController _animationController;
    private  IInteractable _item;

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
        _animationController.SetState(AnimationState.TakeItem);
    }

    public override void Exit() 
    {
        _animationController.SetState(AnimationState.Idle);
    } 
    
    public override void Update()
    {     
        if (_item != null)
    {
        _item.Interact(Character);
        _item = null;
    }

    if (!_animationController.IsAnimationPlaying(AnimationState.TakeItem))
    {
        TryChangeState<IdleState>(); 
    }
        
    }    
    
}
