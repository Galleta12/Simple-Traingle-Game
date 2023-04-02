using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveShootDOWNState : EnemyBaseState
{
    
     private float attackMoveTime;

      
    private float bulletTime;

    private int indexToMove;


    private bool isBossChangePhase;

    public EnemyMoveShootDOWNState(EnemyStateMachine enemystateMachine, bool changePhase) : base(enemystateMachine)
    {
        this.indexToMove =   SelectaPosFromArray(enemystateMachine.ShootsFromDown.Length);
        this.isBossChangePhase = changePhase;   
    }

    public override void Enter()
    {
        //Debug.Log("Now is the down move state");
         enemystateMachine.RB.velocity = Vector2.zero;
        attackMoveTime = enemystateMachine.AttackTimeShoot;
        bulletTime = enemystateMachine.BulletTime;
    }

    public override void FixedTick(float fixeddeltaTime)
    {
        
    }

    public override void Tick(float deltaTime)
    {
        // Vector2 dir = ( enemystateMachine.ShootsFromDown[0].transform.position - enemystateMachine.transform.position).normalized;
        enemystateMachine.transform.position = Vector2.MoveTowards(enemystateMachine.transform.position,
        enemystateMachine.ShootsFromDown[indexToMove].transform.position, enemystateMachine.attackMoveSpeed * deltaTime);

         
        
        float distance = Vector2.Distance(enemystateMachine.transform.position, enemystateMachine.ShootsFromDown[indexToMove].transform.position);
         bulletTime -= deltaTime;
          
          if( distance <= 2){
        
            FacePlayer();
            bulletTime -= deltaTime;
             attackMoveTime -=deltaTime;
            if(bulletTime <=0){
                enemystateMachine.StartShooting();
                bulletTime = enemystateMachine.BulletTime;
            }
            
            if(attackMoveTime <=0){
                
                if(!isBossChangePhase){
                    enemystateMachine.SwitchState(new EnemyZigzagAttackState(enemystateMachine,false));
               }else{
                    enemystateMachine.SwitchState(new EnemyRandomShootsStates(enemystateMachine));
               }
            }
        }
    }

    public override void Exit()
    {
        
    }

 
}
