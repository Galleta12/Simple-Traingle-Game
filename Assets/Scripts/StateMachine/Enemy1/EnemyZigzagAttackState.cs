using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZigzagAttackState : EnemyBaseState
{
    private float remainingAttackTime;

    private bool isBossChangePhase;

    
    public EnemyZigzagAttackState(EnemyStateMachine enemystateMachine, bool changePhase) : base(enemystateMachine)
    {
        this.isBossChangePhase = changePhase;
    }

    public override void Enter()
    {
        remainingAttackTime = enemystateMachine.AttackZigazTime;
    }

    public override void FixedTick(float fixeddeltaTime)
    {
        
       
        if(enemystateMachine.IsTounchingUp && enemystateMachine.GoingUP){
            ChangeDirection();
        }else if(enemystateMachine.IsTounchingDown && !enemystateMachine.GoingUP){
            ChangeDirection();
        }


             if(enemystateMachine.IsTounchingWall){
                if(enemystateMachine.FacingLeft){
                    Flip();
                }else if(!enemystateMachine.FacingLeft){
                    Flip();
                }
            }

        enemystateMachine.RB.velocity = enemystateMachine.attackMoveSpeed* enemystateMachine.attackMoveDirection;

    }

    public override void Tick(float deltaTime)
    {
        remainingAttackTime -= deltaTime;
        if(remainingAttackTime <= 0f){
            
            if(!isBossChangePhase){

                //if it is one shoot from up otherwise shoot from down
                if(RandomState() == 1){
                    enemystateMachine.SwitchState(new EnemyMoveShootUPState(enemystateMachine,false));

                }else{
                    enemystateMachine.SwitchState(new EnemyMoveShootDOWNState(enemystateMachine,false));

                }
            }else{
                enemystateMachine.SwitchState(new EnemyIdleState(enemystateMachine,"random"));
            }
            
            
            

        }
    }



    public override void Exit()
    {
        
    }

}
