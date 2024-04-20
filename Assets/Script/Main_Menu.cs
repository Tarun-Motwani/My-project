using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public AudioClip buttonsound;
    
    private AudioSource audioSource;
    void Start(){
        audioSource = GetComponent<AudioSource>();
        
    }
    public void PlayGame(){
        playbuttonsound();
        SceneManager.LoadSceneAsync(1);
    }
     public void option(){
        playbuttonsound();
     }
    void playbuttonsound(){
        audioSource.PlayOneShot(buttonsound);
    }
    // Start is called before the first frame update
    public void QuitGame(){
        playbuttonsound();
        Application.Quit(); 
    }
}
