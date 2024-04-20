using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void PlayAgain(){
        SceneManager.LoadSceneAsync(1);
    }
    // Start is called before the first frame update
    public void QuitGame(){
        Application.Quit(); 
    }
}
