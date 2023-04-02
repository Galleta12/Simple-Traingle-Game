using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerJumpState : PlayerBaseState
{
    
    
   
    
    public PLayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
        
        stateMachine.setJumpVaribles();
         stateMachine.RB.velocity = new Vector2(stateMachine.RB.velocity.x,stateMachine.intialJumpVelocity);
    }
    public override void FixedTick(float fixeddeltaTime)
    {
       
       
    }

    public override void Tick(float deltaTime)
    {
      
        if(stateMachine.RB.velocity.y <=0){
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
       }
    }

    public override void Exit()
    {
       

    }

   
}


// First attempt of doing a smooth jump

//   public override void Enter()
//     {
//         Debug.Log("This is the jump State");
//         stateMachine.RB.velocity = new Vector2(stateMachine.RB.velocity.x, stateMachine.JumpForce);
//         checkerJump = true;
//         jumpCounter = 0;
//     }
//     public override void FixedTick(float fixeddeltaTime)
//     {
       
//     }

//     public override void Tick(float deltaTime)
//     {
//        if(stateMachine.RB.velocity.y <=0){
//             stateMachine.SwitchState(new PlayerFallState(stateMachine));
//        }

//        if(stateMachine.RB.velocity.y > 0 && checkerJump){
//         jumpCounter += deltaTime;
//         if(jumpCounter > stateMachine.JumpTime) checkerJump =false;

//          float t = jumpCounter / stateMachine.JumpTime;
//          float currentJumpM = stateMachine.JumpMultiplayer;
         
//          //half of the time is
//          //the player will move more slowly
//          if(t > 0.5f){
//             currentJumpM = stateMachine.JumpMultiplayer*(1-t);
//          }

//          stateMachine.RB.velocity+= stateMachine.vecGravity * currentJumpM * deltaTime;

//        }

//        //if you stop holding the jumpbutton
//        if(!stateMachine.InputReader.isJumping){
//         checkerJump = false;
//         jumpCounter = 0;
//        if(stateMachine.RB.velocity.y > 0){
//         //if yo stop pressing jump we want to reduce the velocity of the jump
//         stateMachine.RB.velocity = new Vector2(stateMachine.RB.velocity.x,stateMachine.RB.velocity.y * 0.6f);
//         }
//        }

       
//     }
