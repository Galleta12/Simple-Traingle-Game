using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeamAttackState : EnemyBaseState
{
    
    private int indexToMove;
    
    private float reamingTime;
    
    public EnemyBeamAttackState(EnemyStateMachine enemystateMachine) : base(enemystateMachine)
    {
        this.indexToMove =   SelectaPosFromArray(enemystateMachine.ShootsForBeam.Length);   
    
    }

    public override void Enter()
    {
        
        reamingTime = enemystateMachine.BeamTimeShoot;
    }

    public override void FixedTick(float fixeddeltaTime)
    {
        
    }

    public override void Tick(float deltaTime)
    {
        enemystateMachine.transform.position = Vector2.MoveTowards(enemystateMachine.transform.position,
        enemystateMachine.ShootsForBeam[indexToMove].transform.position, enemystateMachine.attackMoveSpeed * deltaTime);
        float distance = Vector2.Distance(enemystateMachine.transform.position, enemystateMachine.ShootsForBeam[indexToMove].transform.position);
        if(distance <=2){
            reamingTime -=deltaTime;
            FacePlayer();
            enemystateMachine.StartShootingBeam();
            if(reamingTime <=0){
                
               

                    if(RandomStateChangePhase() == 1){
                        enemystateMachine.SwitchState(new EnemyMoveShootDOWNState(enemystateMachine,true));

                    }else if (RandomStateChangePhase() == 2){
                        enemystateMachine.SwitchState(new EnemyMoveShootUPState(enemystateMachine,true));
                    }else{
                        enemystateMachine.SwitchState(new EnemyZigzagAttackState(enemystateMachine,true));
                    }   
            }
        }

    }

    public override void Exit()
    {
        
    }

    
}
