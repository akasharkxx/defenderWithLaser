using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;
    // void Start(){
    //     Debug.Log("Music player start" + GetInstanceID());       
    // }
    void Awake(){
        //Debug.Log("Music player awake" + GetInstanceID()); 
        if(instance != null){
            Destroy(gameObject);
            Debug.Log("Duplicate Destroyed");
        }else{
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
