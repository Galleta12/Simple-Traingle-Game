using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomShootsStates : EnemyBaseState
{
    
   
    
    
    public EnemyRandomShootsStates(EnemyStateMachine enemystateMachine) : base(enemystateMachine)
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
        
        enemystateMachine.transform.position = Vector2.MoveTowards(enemystateMachine.transform.position,
        enemystateMachine.ShootMiddlePosition.position, enemystateMachine.attackMoveSpeed * deltaTime);
        float distance = Vector2.Distance(enemystateMachine.transform.position, enemystateMachine.ShootMiddlePosition.position);


        if(distance<=2){
            for(int i=0; i<20; i++){
            float randomX =   Random.Range(enemystateMachine.RandomPositionBoundaries[0].transform.position.x,
            enemystateMachine.RandomPositionBoundaries[1].transform.position.x);
            Vector3 position = new Vector3(randomX,enemystateMachine.RandomPositionBoundaries[0].transform.position.y,0); 
            enemystateMachine.StartShootingSpikesRandom(position);
            }

            enemystateMachine.SwitchState(new EnemyTargetShootState(enemystateMachine));
        }
        

    }

    public override void Exit()
    {
        
    }

}
