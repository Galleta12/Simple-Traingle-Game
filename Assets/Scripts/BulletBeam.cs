using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBeam : MonoBehaviour
{
    
    
    
    public float speed;
    private Rigidbody2D rb;

    [field: SerializeField] public int damage{get; private set;}
   
    
    [field: SerializeField] public PolygonCollider2D  ColliderBullet {get; private set;}

    private PolygonCollider2D  MyCollider ;

    private List<PolygonCollider2D > alreadyCollideWith = new List<PolygonCollider2D >();

    
    
    // Start is called before the first frame update
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();

      

        GameObject parent = GameObject.Find("EnemyPrefab");
        GameObject child = parent.transform.GetChild(0).gameObject;
        MyCollider = child.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
         transform.position += transform.right * Time.deltaTime * speed;
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
