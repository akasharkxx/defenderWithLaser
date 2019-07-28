using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour{

    public GameObject projectile;
    public float health = 150f;
    public float projectileSpeed = 10f;
    public float shotsPerSecond = 0.5f;
    public int ScoreValue = 150;
    private ScoreKeeper scoreKeeper;

    public AudioClip fireSoundEnemy;

    void Start(){
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();  
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if(missile){
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0){
                Destroy(gameObject);
                scoreKeeper.Score(ScoreValue);
            }
            Debug.Log("Hit by a projectile");
        }
    }
    void Update(){
        float probability= Time.deltaTime * shotsPerSecond;
        if(Random.value < probability){ 
            EnemyFire();
        }
    }

    void EnemyFire(){
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject EnemyBeam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        EnemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }
}
