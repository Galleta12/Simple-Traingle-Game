using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveShootUPState: EnemyBaseState
{
    
    private float attackMoveTime;


    private float bulletTime;
    
    
    private int indexToMove;


    private bool isBossChangePhase;

    public EnemyMoveShootUPState(EnemyStateMachine enemystateMachine, bool changePhase) : base(enemystateMachine)
    {
        this.indexToMove =   SelectaPosFromArray(enemystateMachine.ShootsFromUp.Length);   
        this.isBossChangePhase = changePhase;
    }

    public override void Enter()
    {
        enemystateMachine.RB.velocity = Vector2.zero;
        attackMoveTime = enemystateMachine.AttackTimeShoot;

        bulletTime = enemystateMachine.BulletTime;
    }

    public override void FixedTick(float fixeddeltaTime)
    {
        
    }

    public override void Tick(float deltaTime)
    {
        //Vector2 dir = ( enemystateMachine.ShootsFromUp[0].transform.position - enemystateMachine.transform.position).normalized;
        enemystateMachine.transform.position = Vector2.MoveTowards(enemystateMachine.transform.position,
        enemystateMachine.ShootsFromUp[indexToMove].transform.position, enemystateMachine.attackMoveSpeed * deltaTime);

         
        
        float distance = Vector2.Distance(enemystateMachine.transform.position, enemystateMachine.ShootsFromUp[indexToMove].transform.position);
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
                    if(RandomState() == 1){
                        enemystateMachine.SwitchState(new EnemyMoveShootDOWNState(enemystateMachine,false));

                    }else{
                        enemystateMachine.SwitchState(new EnemyZigzagAttackState(enemystateMachine,false));
                    }

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
