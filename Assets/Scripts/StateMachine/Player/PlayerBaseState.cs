using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    // Start is called before the first frame update
    
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine){
        this.stateMachine = stateMachine;
    }

     protected void OnJump()
    {
        if(stateMachine.IsGrounded()){
          stateMachine.SwitchState(new PLayerJumpState(stateMachine));
        }
    }






protected void handleCoolDownShoot(float deltaTime){

stateMachine.CoolDownShoot -=deltaTime;
if(stateMachine.CoolDownShoot <=0f){
  stateMachine.setCoolDownShoot -=handleCoolDownShoot;
   stateMachine.ShouldShoot = true;
}


}







}
