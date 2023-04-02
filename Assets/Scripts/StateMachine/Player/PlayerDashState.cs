using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{   

     private float remainingDashtTime;
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    
    
    //stateMachine.RB.velocity = Vector2.zero;
    stateMachine.InputReader.DashEvent -= stateMachine.OnDash;
          // the ramining time is the same as the dash time set on the statemachine
    remainingDashtTime = stateMachine.DashTime;

     stateMachine.moveObjectDelegate -= stateMachine.Move;

     stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void FixedTick(float fixeddeltaTime)
    {
        
        
        

        if(stateMachine.isRight){
             
             //stateMachine.RB.AddForce(stateMachine.RB.velocity * stateMachine.DashForce, ForceMode2D.Impulse);
             
             stateMachine.RB.velocity = new Vector2(1 * stateMachine.DashForce  / stateMachine.DashTime, stateMachine.RB.velocity.y);
        }else{
             stateMachine.RB.velocity = new Vector2(-1 * stateMachine.DashForce  / stateMachine.DashTime, stateMachine.RB.velocity.y);
        }
       
    }

    public override void Tick(float deltaTime)
    {
          
          Shadow.me.ShadowSkill();
          remainingDashtTime-= deltaTime;

          if ( remainingDashtTime <= 0f)
        {
           
            
            if(!stateMachine.IsGrounded()){
              
              stateMachine.SwitchState(new PlayerFallState(stateMachine));
              return;
            }

         
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
           
        }

    }

    public override void Exit()
    {
        //since we change state we can subscribe again to the dash, therefore we can trigger this state again
        stateMachine.InputReader.DashEvent += stateMachine.OnDash;
        // we can subscrine to the delegate for the normal movement again
        // I should check this not sure if it is working properly
        stateMachine.moveObjectDelegate += stateMachine.Move;
        
     stateMachine.InputReader.JumpEvent -= OnJump;

    }


    

 
}
