using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

// the max health that it can have each character on the game

// we want to manually set the health of each character


[field:SerializeField] public TMP_Text playerLife {get; private set; }

[field:SerializeField] public TMP_Text enemyLife {get; private set; }
[field: SerializeField] public bool isEnemy {get;private set;}

[field: SerializeField] public bool isPlayer {get;private set;}


[field: SerializeField] public int maxhealth {get;private set;} = 100;
//the current health
private int currentHealth;
//set ivunerable for testing and for blocking
private bool isInvunerable;

// events to check if the player is death or if is taking damage

public event Action OnTakeDamage;
//
public event Action OnDeath;

public event Action OnBossChange;
//bool se to check if is dead

public bool isDead => currentHealth == 0;

private int CounterHit;

public int counterHit{get{return CounterHit;}set{value = CounterHit;}}


private void Start() {
   currentHealth = maxhealth;
}



public void setInvunerable(bool isInvunerable){
   this.isInvunerable = isInvunerable;
}

public void DealDamage(int damage){
    // if the health is 0, we want dont want to do anything
    if(currentHealth == 0){return;}
    // if the character is invunerable we don't want to do anything
    if(isInvunerable){return;}



    currentHealth = Mathf.Max(currentHealth - damage,0);

    
    //trigger the on take damage action and on death action for the enemy state machine
    
    OnTakeDamage?.Invoke();
    //count how many times it was hit
    CounterHit++;
    
    //this means that the game has finish hence we can just load to another scene
    if(currentHealth == 0){
         if(isEnemy){
               int currentHealth = get_current_health();
               enemyLife.text = ("Enemy: ") + currentHealth.ToString();
               //SceneManager.LoadScene("WinScene"); 
         }
         if(isPlayer){
            int currentHealth = get_current_health();
            playerLife.text = ("Player: ") + currentHealth.ToString();
            //SceneManager.LoadScene("GameOverScene"); 

         }
      
      
      
      
      OnDeath?.Invoke();
    }
    
    if(isEnemy){
      if(currentHealth <= maxhealth/2){
         OnBossChange?.Invoke();
      }

    }
     
   

}


private void Update()
{
    
  
   
}



// private void gameFinish(){
//    GameObject player = GameObject.FindWithTag("Player");
//    GameObject enemy= GameObject.FindWithTag("Enemy");

//    Debug.Log("This is the " + player);
//    Debug.Log("This is the " + enemy);

//       if(!player){

//           Debug.Log("You have lost");
//           //go to the lost menu
//           SceneManager.LoadScene("GameOverScene");

//       }
//       if(!enemy){
//           Debug.Log("You won");
//           //go to win meu
//           SceneManager.LoadScene("WinScene");

//       }
// }


public int get_current_health(){
   return currentHealth;
}

   
}
