using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetShootState : EnemyBaseState
{
    
   
     private float remainingTime;
    
    public EnemyTargetShootState(EnemyStateMachine enemystateMachine) : base(enemystateMachine)
    {
         remainingTime = enemystateMachine.TimePerRandomandTargetState;
    }

    public override void Enter()
    {
    }

    public override void FixedTick(float fixeddeltaTime)
    {
        
    }

    public override void Tick(float deltaTime)
    {
        
        remainingTime -= deltaTime;
        if(remainingTime <= 0){
            enemystateMachine.transform.position = Vector2.MoveTowards(enemystateMachine.transform.position,
            enemystateMachine.ShootMiddlePosition.position, enemystateMachine.attackMoveSpeed * deltaTime);
            float distance = Vector2.Distance(enemystateMachine.transform.position, enemystateMachine.ShootMiddlePosition.position);

            if(distance<=2){
                //I will shoot only two of this instances then I will change state to the idle state;
            
                
                enemystateMachine.StartShootingTargets();

            
                enemystateMachine.SwitchState(new EnemyIdleTargetState(enemystateMachine));
            }

        }   

    }
    public override void Exit()
    {
        
    }

}
