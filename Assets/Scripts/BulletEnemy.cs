using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;


   private GameObject Player ;

    [field: SerializeField] public int damage{get; private set;}
    
    
      private PolygonCollider2D  MyCollider;
    [field: SerializeField] public PolygonCollider2D  ColliderBullet {get; private set;}
// this is to keep track of the object that collide with the sword
    private List<PolygonCollider2D > alreadyCollideWith = new List<PolygonCollider2D >();
    
    private void Start() {
        rb= GetComponent<Rigidbody2D>();

        Player = GameObject.Find("PlayerTri");

        GameObject parent = GameObject.Find("EnemyPrefab");
        GameObject child = parent.transform.GetChild(0).gameObject;
        MyCollider = child.GetComponent<PolygonCollider2D>();
        
        
        Vector3 dir = (Player.transform.position - transform.position).normalized;

        rb.AddForce(dir * speed,ForceMode2D.Impulse);


        
      
    }

    // Update is called once per frame
   private void Update()
    {
        
        // Vector3 dir = (Player.transform.position - transform.position).normalized;
        
        // transform.position += dir * Time.deltaTime * speed;
    }

    private void FixedUpdate() {
        
    }


     private void OnTriggerEnter2D(Collider2D other)
    {
    
    
    if(other == ColliderBullet){return;}
     if(other == MyCollider){return;}


    // we want to get the health component of the object that we collide with
    if(other.TryGetComponent<Health>(out Health health)&& other.gameObject.tag == "Player"){
    // we want to pass the damage and the knockback, therefore we can hanlde the impact state
    // we pass the direction for the knockback
        
        
        health.DealDamage(damage);
        GameObject.Destroy(gameObject);    
    }
    else if (other.gameObject.tag == "Boundary"){
            GameObject.Destroy(transform.gameObject);

    }
    
    }


    
    private void OnBecameInvisible()
    {
       GameObject.Destroy(transform.gameObject);
    }
}
