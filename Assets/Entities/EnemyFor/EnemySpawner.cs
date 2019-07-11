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
    public float spawnDelay = 0.5f;
    float xmin;
    float xmax;
    bool movingLeft = false;

    // Start is called before the first frame update
    void Start(){
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
        xmin = leftBoundary.x + padding;
        xmax = rightBoundary.x - padding;
        SpawnEnemies();
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
        if(leftEdgeOfTransformation < xmin){
            movingLeft = false;
        }else if( rightEdgeOfTransformation > xmax){
            movingLeft = true;
        }
        if(AllMembersDead()){
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
    }

    Transform NextFreePosition(){
        foreach (Transform childPositionGameObject in transform){
            if(childPositionGameObject.childCount == 0){
                return childPositionGameObject;
            }
        }
        return null;
    }

    bool AllMembersDead(){
        // transform.childCount; 
        foreach (Transform childPositionGameObject in transform){
            if(childPositionGameObject.childCount > 0){
                return false;
            }
        }
        return true;
    }
    void SpawnEnemies(){
        foreach(Transform child in transform){
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull(){
        Transform freePosition = NextFreePosition();
        if(freePosition){
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition()){
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }
}
