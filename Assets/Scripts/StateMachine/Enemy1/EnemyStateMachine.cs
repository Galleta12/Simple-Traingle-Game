using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EnemyStateMachine : StateMachine
{
    
    
    
    
    [field:SerializeField] public TMP_Text enemyLife {get; private set; }


    [field: SerializeField] public AudioSource AudioPlayer {get; private set; }
    
    [field: SerializeField] public Rigidbody2D RB {get; private set; }

    
    [field: SerializeField] public SpriteRenderer[] sprite {get; private set; }
    
    [field: SerializeField] public PolygonCollider2D ColliderEnemy {get; private set; }

    [field: SerializeField] public GameObject Player {get; private set; }
    
    [field:SerializeField] public Health Health {get; private set; }

    
    [field: SerializeField] public GameObject NormalBullet {get; private set; }

    
    [field: SerializeField] public GameObject BeamBullet {get; private set; }
    

    [field: SerializeField] public GameObject TargetBulletSlow {get; private set; }

    [field: SerializeField] public GameObject TargetBulletFast {get; private set; }


    [field: SerializeField] public GameObject SpikeBullet {get; private set; }

    
    [field: SerializeField] public GameObject EnemyShootPosition {get; private set; }

    [field: SerializeField] public Transform ShootMiddlePosition {get; private set; }

    [field: SerializeField] public Transform[] ShootsFromUp {get; private set; }


    [field: SerializeField] public Transform[] ShootsFromDown {get; private set; }


    [field: SerializeField] public Transform[] ShootsForBeam {get; private set; }



    [field: SerializeField] public Transform[] RandomPositionBoundaries {get; private set; }

  [field: SerializeField] public Transform groundCheckUp {get; private set; }

  [field: SerializeField] public Transform groundCheckDown {get; private set; }

  [field: SerializeField] public Transform groundCheckWall {get; private set; }

    [field: SerializeField] public float groundCheckRadius {get; private set; }

     [field: SerializeField] public LayerMask groundLayer {get; private set; }


    [field: SerializeField] public float attackMoveSpeed {get; private set; }
    [field: SerializeField] public Vector2 attackMoveDirection; 

    [field: SerializeField] public float burstSpeed {get; private set; }
     
    
    [field: SerializeField] public float AttackZigazTime {get; private set; }


    [field: SerializeField] public float BeamTimeShoot {get; private set; }

    [field: SerializeField] public float AttackTimeShoot {get; private set; }

    [field: SerializeField] public float BulletTime {get; private set; }

    [field: SerializeField] public float BurstTime {get; private set; }


    [field: SerializeField] public float ShootRandomAndTargetTime {get; private set; }

     [field: SerializeField] public float NormalIdleStateTime {get; private set; }


    [field: SerializeField] public float TimePerRandomandTargetState {get; private set; }


     [field: SerializeField] public int AttackForce {get; private set; }


    //public UnityEvent gameOver;
  



  private float coolDownAttackZigazTime =0;

  public float CoolDownAttackZigaz{get{return coolDownAttackZigazTime;}set{coolDownAttackZigazTime=value;}}


    private bool isTounchinup;

    public bool IsTounchingUp{get{return isTounchinup;}set{isTounchinup = value;}}

    private bool isTounchingDown;

    public bool IsTounchingDown{get{return isTounchingDown;}set{isTounchingDown=value;}}


    private  bool isTounchingWall;

    public bool IsTounchingWall{get{return isTounchingWall;}set{isTounchingWall=value;}}


    
 


    //variable to detect where is facing

    private  bool goingUP = true;

    public bool GoingUP{get{return goingUP;}set{goingUP =value;}}

    private bool facingLeft = true;

    public bool FacingLeft{get{return facingLeft;}set{facingLeft = value;}}
  
   

   
    
    
 


    private void OnEnable() {
        Health.OnDeath +=HandleDeath;
      
    }

   

    private void OnDisable() {
       
        Health.OnDeath -=HandleDeath;
        
    }
     
    
    
    
    private void Start() {
       
      
      Health.OnBossChange += changeBoosState;
      attackMoveDirection.Normalize();


       //SwitchState(new EnemyTargetShootState(this));
       SwitchState(new EnemyZigzagAttackState(this,false));
       

        
    }

   

    private void changeBoosState()
    {
     
      SwitchState(new EnemyIdleState(this,"beam"));
       //as soon as this happen we want to change state and unsubscribe from the event
      Health.OnBossChange -= changeBoosState;
    }

    public void StartShooting()
    {
      Instantiate(NormalBullet,EnemyShootPosition.transform.position, EnemyShootPosition.transform.rotation);
    }

    public void StartShootingBeam()
    {
      Instantiate(BeamBullet,EnemyShootPosition.transform.position, EnemyShootPosition.transform.rotation);

    }


    public void StartShootingTargets()
    {
     
        Instantiate(TargetBulletSlow,EnemyShootPosition.transform.position, EnemyShootPosition.transform.rotation);
        Instantiate(TargetBulletFast,EnemyShootPosition.transform.position, EnemyShootPosition.transform.rotation);
      
      

    }


    public void StartShootingSpikesRandom(Vector3 position)
    {
     
      Instantiate(SpikeBullet,position,SpikeBullet.transform.rotation);
      

    }

    public override void CustomUpdate(float deltatime){

         
         int currentHealth = Health.get_current_health();
          enemyLife.text = ("Enemy: ") + currentHealth.ToString(); 
       
         
         isTounchinup = Physics2D.OverlapCircle(groundCheckUp.position,groundCheckRadius,groundLayer);
         isTounchingDown = Physics2D.OverlapCircle(groundCheckDown.position,groundCheckRadius,groundLayer);
         isTounchingWall = Physics2D.OverlapCircle(groundCheckWall.position,groundCheckRadius,groundLayer);
      
       
    }


 

    public override void CustomFixedUpdate(float fixedDeltatime){

    
    }


   private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(groundCheckUp.position,groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position,groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position,groundCheckRadius);

        Gizmos.DrawLine(transform.position,Player.transform.position);
        

    }


    
    private void OnTriggerEnter2D(Collider2D other)
    {
    
    
    if(other == ColliderEnemy){return;}
    
    // we want to get the health component of the object that we collide with
    if(other.TryGetComponent<Health>(out Health health)&& other.gameObject.tag == "Player"){
    // we want to pass the damage and the knockback, therefore we can hanlde the impact state
    // we pass the direction for the knockback
        
        
        health.DealDamage(AttackForce);
        
    }

    else if(other.gameObject.tag == "PlayerBullet"){
      StartCoroutine(FlashRed());
      AudioPlayer.Play();

    }
    }

    private IEnumerator FlashRed()
    {
      
    
      
      foreach (SpriteRenderer new_sprite in sprite){
        
      
        new_sprite.color = Color.red;

      }
      yield return new WaitForSeconds(0.1f);
      foreach (SpriteRenderer new_sprite in sprite){
        
        new_sprite.color = Color.blue;

      }
    }


    private void HandleDeath(){
     //GameObject.Destroy(transform.gameObject);
     //we can just change scence an everything will restart
     
      
      Time.timeScale = 0;
      //gameOver.Invoke();
      SceneManager.LoadScene("WinScene",LoadSceneMode.Single); 
  }
}
