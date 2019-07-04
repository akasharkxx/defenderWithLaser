using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 15.0f;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }else if (Input.GetKey(KeyCode.RightArrow)){
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
