using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    [field: SerializeField] public int damage{get; private set;}
    




    [field: SerializeField] public PolygonCollider2D  ColliderBullet {get; private set;}

     private PolygonCollider2D  MyCollider ;

     

    private List<PolygonCollider2D > alreadyCollideWith = new List<PolygonCollider2D >();

    
    private void Start() {
        
        
        MyCollider = GameObject.Find("PlayerTri").GetComponent<PolygonCollider2D>();

        rb= GetComponent<Rigidbody2D>();
        //rb.AddForce(transform.right * speed,ForceMode2D.Impulse);
        //stateMachine.RB.velocity = new Vector2(horizontal * stateMachine.Speed, stateMachine.RB.velocity.y);
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    
    //Debug.Log("This is the other" + other);
    if(other == ColliderBullet){return;}
     if(other == MyCollider){return;}


    // we want to get the health component of the object that we collide with
    if(other.TryGetComponent<Health>(out Health health)){
    // we want to pass the damage and the knockback, therefore we can hanlde the impact state
    // we pass the direction for the knockback
        
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "TargetBullet" || other.gameObject.tag == "SpikeBullet"){

        
            health.DealDamage(damage);
            
         
            
            
            GameObject.Destroy(transform.gameObject);
            
        }
        
    }else if (other.gameObject.tag == "Boundary"){
            GameObject.Destroy(transform.gameObject);

    }

    }


    private void OnBecameInvisible()
    {
       GameObject.Destroy(transform.gameObject);
    }

  

}
