using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 15.0f;
    public float padding = 1.0f;
    
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 0.2f;
    
    public AudioClip fireSound;

    public float PlayerHealth = 250f;
    float xmin;
    float xmax;
    
    void Start(){
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }
    void Fire(){
        Vector3 startPosition = transform.position + new Vector3(0, 0, 0);
        GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);        
    }
    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("Fire");
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            // this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;

        }else if (Input.GetKey(KeyCode.RightArrow)){
            // this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // restricting to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D collider) {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if(missile){
            Debug.Log("Player got HIT!!!");
            PlayerHealth -= missile.GetDamage();
            missile.Hit();
            if (PlayerHealth <= 0){
                Destroy(gameObject);
            }
        }
    }
}
