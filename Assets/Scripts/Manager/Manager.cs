using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    


    //add a little freezing at the begging to start only looking after 30 seconds has passed

    private float remainingTime =30;


   private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
      

        
        remainingTime -= Time.deltaTime;
        if(remainingTime <= 0){
            GameObject player = GameObject.FindWithTag("Player");
            GameObject enemy= GameObject.FindWithTag("Enemy");

            //   Debug.Log("This is the " + player);
            // Debug.Log("This is the " + enemy);

            if(!player){

                // Debug.Log("You have lost");
                //go to the lost menu
                SceneManager.LoadScene("GameOverScene");

            }
            if(!enemy){
                // Debug.Log("You won");
                //go to win meu
                SceneManager.LoadScene("WinScene");

            }

        }
        

    }
}
