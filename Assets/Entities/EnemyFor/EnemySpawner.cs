using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 7.0f;
    public float padding = 1.0f;

    float xmin;
    float xmax;
    bool movingLeft = false;

    // Start is called before the first frame update
    void Start(){
        foreach(Transform child in transform){
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child  ;
        }
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
        xmin = leftBoundary.x + padding;
        xmax = rightBoundary.x - padding;
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update(){
        if(movingLeft){
            transform.position += Vector3.left * speed * Time.deltaTime;
        }else{
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        float rightEdgeOfTransformation = transform.position.x + 0.5f*width;
        float leftEdgeOfTransformation = transform.position.x - 0.5f*width;
        if(leftEdgeOfTransformation < xmin || rightEdgeOfTransformation > xmax){
            movingLeft = !movingLeft;
        }
    }
}
