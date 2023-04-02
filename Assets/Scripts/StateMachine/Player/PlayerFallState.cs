using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
       
    }
    public override void FixedTick(float fixeddeltaTime)
    {
       
    }

    public override void Tick(float deltaTime)
    {
          
        stateMachine.RB.velocity-= stateMachine.vecGravity * stateMachine.FallMultiplayer * deltaTime;
        if(stateMachine.IsGrounded()){
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
      
    }

    public override void Exit()
    {
        
    }

}
