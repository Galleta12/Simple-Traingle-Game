using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateMachine : StateMachine
{
  
//



[field:SerializeField] public TMP_Text playerLife {get; private set; }


[field:SerializeField] public InputReader InputReader {get; private set; }


[field:SerializeField] public SpriteRenderer sprite {get; private set; }


[field:SerializeField] public GameObject Explosion {get; private set; }

[field:SerializeField] public Health Health {get; private set; }

[field:SerializeField] public Rigidbody2D RB {get; private set; }

[field:SerializeField] public PolygonCollider2D MyCollider {get; private set; }

[field:SerializeField] public Transform GroundCheck {get; private set; }

[field:SerializeField] public LayerMask GroundLayer {get; private set; }




[field:SerializeField] public Transform Gun {get; private set; }

[field:SerializeField] public GameObject ShootPosition {get; private set; }


[field:SerializeField] public GameObject Bullet {get; private set; }


[field:SerializeField] public GameObject BulletRecoil {get; private set; }


//dash variables
[field: SerializeField] public float DashForce {get; private set; }


[field: SerializeField] public float DashTime {get; private set; }


[field: SerializeField] public float RecoilTime {get; private set; }


[field:SerializeField] public float maxJumpHeight {get;private set;}

[field:SerializeField]  public float maxJumpTime {get;private set;}


// ---------------------------------------variables to set-----------------------------------//

[field:SerializeField] public float RecoilShootSpeed {get; private set; }



[field:SerializeField] public float FallMultiplayer {get; private set; }


[field:SerializeField] public float ShootTime {get; private set; }




  //to get the mouse position 
  public Camera MainCamera {get; private set;}

  //--------------------delegate variables----------------------------------

public delegate void MoveObject(float deltaTime);

public MoveObject moveObjectDelegate;


// delegates to set the cool down
public delegate void SetCoolDown(float deltaTime);

public SetCoolDown setCoolDown;


public delegate void SetCoolDownShoot(float deltaTime);

public SetCoolDown setCoolDownShoot;

// variables ----------------------------



public bool isRight {get; private set;}


private float coolDownTimeShoot =0;

public float CoolDownShoot{get{return coolDownTimeShoot;}set{coolDownTimeShoot=value;}}


private float horizontalMove;

public float HorizontalMove{get{return horizontalMove;}set{horizontalMove = value;}}


private bool shouldShoot = true;

public bool ShouldShoot{get{return shouldShoot;}set{shouldShoot=value;}}



public Vector2 vecGravity {get; private set; }



public float gravity {get; private set; } = -9.8f;


public float intialJumpVelocity {get; private set; }

public Color colorSprite {get;private set;}








  private void OnEnable() {
        Health.OnDeath +=HandleDeath;
      
    }

   

    private void OnDisable() {
       
        Health.OnDeath -=HandleDeath;
        
    }
     



private void Start() {
    
    //save color of sprite
    colorSprite = sprite.color;


    vecGravity = new Vector2(0, -Physics2D.gravity.y);
    SwitchState(new PlayerIdleState(this));    
    MainCamera = Camera.main;

    moveObjectDelegate += Move;

     // suscribe to an event of the dash
    InputReader.DashEvent += OnDash;

    InputReader.ShootEvent += OnShoot;

    setJumpVaribles();

    StartCoroutine("Shoot",.7f);
    
}

  
  
  private IEnumerator Shoot(float delay){
    while(true){
      yield return new WaitForSeconds(delay);
      ShootPerSecond();
    }
  }

  private void ShootPerSecond()
  {
    if(ShouldShoot){
        Instantiate(Bullet,ShootPosition.transform.position, ShootPosition.transform.rotation);

    }
  }


  public void ShootRecoil()
  {
    
        Instantiate(BulletRecoil,ShootPosition.transform.position, ShootPosition.transform.rotation);

    
  }

  public void ExplosionRecoil(){
    Instantiate(Explosion,ShootPosition.transform.position,ShootPosition.transform.rotation);
  }

  public override void CustomFixedUpdate(float fixedDeltatime)
    {
      //call the delegate if it exists
      moveObjectDelegate?.Invoke(fixedDeltatime);
     
      
    }

    public override void CustomUpdate(float deltatime){
      
    
      
      rotateGun();
      
      
      int currentHealth = Health.get_current_health();
      playerLife.text = ("Player: ") + currentHealth.ToString(); 

      

      // call the set cool down delegate if it exists
      
      setCoolDownShoot?.Invoke(deltatime);

      
 
      
    }


  


   

    public void Move(float fixedDeltatime){
        
        
      
        
        RB.velocity = new Vector2(HorizontalMove, RB.velocity.y);
    }



    //we always want to move the gun to look around so we can have the function here

    private void rotateGun(){
      Vector2 dir = Input.mousePosition - MainCamera.WorldToScreenPoint(transform.position);
      float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
      Gun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      //Debug.Log(angle);;

     //check if if its the mouse right or left
     if(angle > 100 || angle < -100){
        isRight =false;
     }else{
      isRight = true;
      
     }

      

    }


 


  public void OnDash(){
    
       SwitchState(new PlayerDashState(this));
  
    
  }


  public void OnShoot(){
    if(CoolDownShoot <= 0f){
       Vector2 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
      Vector2 moveDirection =  mousePos - (Vector2)transform.position ;
       SwitchState(new PlayerShootState(this,moveDirection));
  }
    
  }


public void setJumpVaribles(){
   float timeToApex =maxJumpTime / 2;
    gravity = (-2 * maxJumpHeight) / MathF.Pow(timeToApex, 2);
    intialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
}



  public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }




  private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;

      // Gizmos.DrawLine(-directionPosPlayer,GroundCheck.transform.position);



  }


  private void HandleDeath(){
      //GameObject.Destroy(transform.gameObject);
      
      Time.timeScale = 0;
     
      
    
        
        SceneManager.LoadScene("GameOverScene",LoadSceneMode.Single); 


      
      
      
  }

 




  private void OnTriggerEnter2D(Collider2D other)
  {
    

    if(other == MyCollider){return;}
    
   
    if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "TargetBullet" || 
    other.gameObject.tag == "SpikeBullet" ||  other.gameObject.tag == "EnemyBullet"){
      StartCoroutine(FlashRed());
    }
    


  }

     private IEnumerator FlashRed()
    {
      
        sprite.color = Color.red;

      
        yield return new WaitForSeconds(0.1f);
     
        sprite.color = colorSprite;

      
    }




}
