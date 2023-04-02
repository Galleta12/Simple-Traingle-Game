using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesRandom : MonoBehaviour
{
    //this will just fall with the default gravity settings of the rb

    [field: SerializeField] public int damage{get; private set;}


    [field: SerializeField] public PolygonCollider2D  ColliderBullet {get; private set;}

    private GameObject floor;


    [field: SerializeField] public Health Health{get; private set;}

    private PolygonCollider2D  MyCollider ;

    private GameObject Player ;

   
   
     private void OnEnable() {
        Health.OnDeath +=HandleDeath;
      
    }

   

    private void OnDisable() {
       
        Health.OnDeath -=HandleDeath;
        
    }

      private void HandleDeath()
    {
        GameObject.Destroy(transform.gameObject);
    }
     
   
   
   
   private void Start()
    {
         Player = GameObject.Find("PlayerTri");

        GameObject parent = GameObject.Find("EnemyPrefab");
        GameObject child = parent.transform.GetChild(0).gameObject;
        floor = GameObject.Find("FloorTest");
        MyCollider = child.GetComponent<PolygonCollider2D>();

    }


     private void OnTriggerEnter2D(Collider2D other)
    {
    
  
    if(other == ColliderBullet){return;}
     if(other == MyCollider){return;}


    // we want to get the health component of the object that we collide with
    if(other.TryGetComponent<Health>(out Health health) && other.gameObject.tag == "Player"){
    // we want to pass the damage and the knockback, therefore we can hanlde the impact state
    // we pass the direction for the knockback
        
        health.DealDamage(damage);
        
         GameObject.Destroy(transform.gameObject);
        
    }
    else if (floor.GetComponent<BoxCollider2D>() == other){
        //Destroy if it collides with anythin else
         GameObject.Destroy(transform.gameObject);
    }
    }


    private void OnBecameInvisible()
    {
      GameObject.Destroy(transform.gameObject);
    }

  
}
