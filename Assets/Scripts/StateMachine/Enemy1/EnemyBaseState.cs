using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    

    protected EnemyStateMachine enemystateMachine;



    public EnemyBaseState(EnemyStateMachine  enemystateMachine){
        this.enemystateMachine= enemystateMachine;
      
    }

    protected void ChangeDirection(){
        //change if it is true or not
        enemystateMachine.GoingUP = !enemystateMachine.GoingUP;

        enemystateMachine.attackMoveDirection.y *= -1;
    }


    protected void Flip(){
        enemystateMachine.FacingLeft = !enemystateMachine.FacingLeft;
        
        enemystateMachine.attackMoveDirection.x *= -1;
        enemystateMachine.transform.Rotate(0,180,0);
    }


     protected void FacePlayer(){
        float dir = enemystateMachine.Player.transform.position.x - enemystateMachine.transform.position.x;
        if(dir > 0 &&  enemystateMachine.FacingLeft){
            Flip();
        } else if(dir < 0 && ! enemystateMachine.FacingLeft){
            Flip();
        }
    }



    protected int RandomState(){
        //the 3 is exclusive
        int random = Random.Range(1,3);
        return random;
    }

     protected int RandomStateChangePhase(){
        //the 3 is exclusive
        int random = Random.Range(1,4);
        return random;
    }


    protected int SelectaPosFromArray(int lenght){
        int randomIndex = Random.Range(0,lenght);
        return randomIndex;

    }
   



}
