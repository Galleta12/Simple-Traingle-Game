using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    
    
   
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
      stateMachine.InputReader.JumpEvent += OnJump;
    }


    public override void FixedTick(float fixeddeltaTime)
    {
         
    }

    public override void Tick(float deltaTime)
    {
         
        //Debug.Log(stateMachine.InputReader.ScroolWheel.y);
        
    if(!stateMachine.IsGrounded()){
      stateMachine.SwitchState(new PlayerFallState(stateMachine));
    }
        
        
        

    }

    public override void Exit()
    {
      stateMachine.InputReader.JumpEvent -= OnJump;

    }

  

    
}
