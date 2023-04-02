using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    
     private float remaingTime;
    

    private string name;

     
   

    
    public EnemyIdleState(EnemyStateMachine enemystateMachine, string nameofState) : base(enemystateMachine)
    {
       
       this.name = nameofState;
       
    
    
    }

    public override void Enter()
    {
        
        remaingTime = enemystateMachine.NormalIdleStateTime;

    }

    public override void FixedTick(float fixeddeltaTime)
    {
        enemystateMachine.RB.velocity = Vector2.zero;
    }

    public override void Tick(float deltaTime)
    {
        remaingTime -= deltaTime;
        if(remaingTime <=0){
            
            
                if(name == "shootup"){
                 enemystateMachine.SwitchState(new EnemyMoveShootUPState(enemystateMachine,true));
                }else if(name == "shootdown"){
                 enemystateMachine.SwitchState(new EnemyMoveShootDOWNState(enemystateMachine,true));
                }else if(name == "zigzag"){
                 enemystateMachine.SwitchState(new EnemyZigzagAttackState(enemystateMachine,true));
                }else if(name == "random"){
                    enemystateMachine.SwitchState(new EnemyRandomShootsStates(enemystateMachine));
                }else if(name == "beam"){
        
                 enemystateMachine.SwitchState(new EnemyBeamAttackState(enemystateMachine));
                }
            
            
            
        }
    }
    public override void Exit()
    {
        
    }

}
