using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Volume : MonoBehaviour
{
    
    public AudioMixer background_music;
    public Slider background_slider;
    private void Start(){
        if(PlayerPrefs.HasKey("musicVolume")){
            LoadVolume();
        }
        else{
            setvolume();
        }
    }
    public void setvolume(){
        float Volume = background_slider.value;
        background_music.SetFloat("b_music",Mathf.Log10(Volume)*20);
        PlayerPrefs.SetFloat("musicVolume",Volume);
    }
    public void LoadVolume(){
     background_slider.value=PlayerPrefs.GetFloat("musicVolume");
     setvolume();
    }
}
