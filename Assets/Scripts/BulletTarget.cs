using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    
    
   
    public float speed;
    private Rigidbody2D rb;

   
    [field: SerializeField] public Health Health{get; private set;}

    [field: SerializeField] public int damage{get; private set;}
    

    [field: SerializeField] public PolygonCollider2D  ColliderBullet {get; private set;}

    private PolygonCollider2D  MyCollider ;

    private GameObject Player ;

     
     
    private void OnEnable() {
        Health.OnDeath +=HandleDeath;
      
    }

   

    private void OnDisable() {
       
        Health.OnDeath -=HandleDeath;
        
    }
     
     
     
     private void Start()
    {
       
        rb= GetComponent<Rigidbody2D>();
         Player = GameObject.Find("PlayerTri");

        GameObject parent = GameObject.Find("EnemyPrefab");
        GameObject child = parent.transform.GetChild(0).gameObject;
        MyCollider = child.GetComponent<PolygonCollider2D>();

       
    }

    private void Update() {
        Vector2 dir = (Player.transform.position - transform.position).normalized;
         float angle = MathF.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
         
       

       
            transform.position = Vector2.MoveTowards(transform.position,
            Player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);



    }


    private void FixedUpdate() {
     
    }

     private void HandleDeath()
    {
        GameObject.Destroy(transform.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    
  
    if(other == ColliderBullet){return;}
     if(other == MyCollider){return;}


    // we want to get the health component of the object that we collide with
    if(other.TryGetComponent<Health>(out Health health) &&  other.gameObject.tag == "Player"){
    // we want to pass the damage and the knockback, therefore we can hanlde the impact state
    // we pass the direction for the knockback
        Vector2 direction = (other.transform.position - ColliderBullet.transform.position).normalized;
        
        health.DealDamage(damage);
        
         GameObject.Destroy(transform.gameObject);
        
        }
       
   
    
    }


    


    
}
