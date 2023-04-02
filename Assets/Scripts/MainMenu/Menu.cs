using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    

    public void PlayGame(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void SeeControlsMenu(){
        SceneManager.LoadScene("MenuControl");
    }

    public void QuitGame(){
        Application.Quit();
    }


     public void GetBack(){
       SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame(){
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");

    }

    
  
}
