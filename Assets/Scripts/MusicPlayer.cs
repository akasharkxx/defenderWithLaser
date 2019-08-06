using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    void Start(){
        //Debug.Log("Music player awake" + GetInstanceID()); 
        if(instance != null && instance != this){
            Destroy(gameObject);
            Debug.Log("Duplicate Destroyed");
        }else{
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    void OnLevelWasLoaded(int level){
        Debug.Log("Music Player Loaded Level : " + level);
        music.Stop();
        if(level == 0){
            music.clip = startClip;
        }else if(level == 1){
            music.clip = gameClip;
        }else if(level == 2){
            music.clip = endClip;
        }
        music.loop = true;
        music.Play();
    }
}
