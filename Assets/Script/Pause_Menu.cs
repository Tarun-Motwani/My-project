using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause_Menu : MonoBehaviour
{
   
    [SerializeField] GameObject pauseMenu;
    public void pause(){
        
        pauseMenu.SetActive(true);
        Time.timeScale=0;
        
    }
    public void resume(){
        pauseMenu.SetActive(false);
        Time.timeScale=1;
        
    }
    
    public void back_to_menu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale=1;
        
    }
    public void Main_Menu(){

        SceneManager.LoadScene(0);


    }
    public void quit(){
        Application.Quit();
        
    }
}
