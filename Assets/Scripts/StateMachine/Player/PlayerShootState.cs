using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerBaseState
{
    
    
   private Vector2 Direction;

    private float remainingTime;
    

    private float distance;

    public PlayerShootState(PlayerStateMachine stateMachine, Vector2 direction) : base(stateMachine)
    {
     this.Direction = direction;
     
    }

    public override void Enter()
    {
     

     
     
     remainingTime = stateMachine.RecoilTime;
     
     stateMachine.moveObjectDelegate -= stateMachine.Move;
     
     stateMachine.InputReader.ShootEvent -= stateMachine.OnShoot;
     stateMachine.ShouldShoot = false;
     stateMachine.ShootRecoil();
    
       // Debug.Log("This is the vector direction"+ moveDirection);
    //stateMachine.RB.AddForce(-(Direction.normalized * stateMachine.Speed), ForceMode2D.Impulse);
   stateMachine.ExplosionRecoil();
     
    }

    public override void FixedTick(float fixeddeltaTime)
    {

     stateMachine.RB.velocity = -(Direction.normalized * stateMachine.RecoilShootSpeed)/stateMachine.RecoilTime;

    


    }

    public override void Tick(float deltaTime)
    {
      
       remainingTime -= deltaTime;
       if(remainingTime <= 0){
        
          
        
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        
       }
   
        
    }
    public override void Exit()
    {
        stateMachine.InputReader.ShootEvent += stateMachine.OnShoot;

        stateMachine.CoolDownShoot = stateMachine.ShootTime;

        stateMachine.setCoolDownShoot += handleCoolDownShoot;

       stateMachine.moveObjectDelegate += stateMachine.Move;
       

       
    }

}
