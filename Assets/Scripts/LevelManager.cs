using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string name){
        Debug.Log("Level load requested for: " + name);
        Application.LoadLevel(name);
    }
    public void QuitRequest(){
        Debug.Log("Quit");
        Application.Quit();
    }
    // Imported from Block Breaker
    // public void LoadNextLevel(){
    //     Application.LoadLevel(Application.loadedLevel + 1);
    // }

    // public void BrickDestroyed(){
    //     if(Bricks.breakableCount <= 0){
    //         LoadNextLevel();
    //     }
    // }
}
