using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleTargetState : EnemyBaseState
{
   
     private float remaingTime;
    

    

     
    
    public EnemyIdleTargetState(EnemyStateMachine enemystateMachine) : base(enemystateMachine)
    {
       
    
    }

    public override void Enter()
    {
        //Debug.Log("This is the idle state for the enemy after the target shoot");   
        remaingTime = enemystateMachine.ShootRandomAndTargetTime;

    }

    public override void FixedTick(float fixeddeltaTime)
    {
        enemystateMachine.RB.velocity = Vector2.zero;
    }

    public override void Tick(float deltaTime)
    {
        remaingTime -= deltaTime;
        if(remaingTime <=0){
            enemystateMachine.SwitchState(new EnemyBeamAttackState(enemystateMachine));
        }
    }
    public override void Exit()
    {
        
    }

}
