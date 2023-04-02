using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    
    
    
    
    private Controls controls;
   


    public Vector3 MousePOS {get; private set;}

  

 

    // Events actions varaibles
    public event Action JumpEvent;

    public event Action DashEvent;
   
   public event Action ShootEvent;

    
    private void Start()
    {
        //stroe instance of class controls
        controls = new Controls();
        //reference to this class
        controls.Player.SetCallbacks(this);
        //enable it
        
        controls.Player.Enable();

        
    }
    
    
    public void OnJump(InputAction.CallbackContext context)
    {
      if(!context.performed){return;}
      JumpEvent?.Invoke();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
     if(!context.performed){return;}
     ShootEvent?.Invoke();     
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       MousePOS = context.action.ReadValue<Vector2>();
      
      
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
       
    }

    public void OnDash(InputAction.CallbackContext context)
    {
       if(!context.performed){return;}
       DashEvent?.Invoke();
    }


    public void  OnDisable()
    {
      controls.Player.Disable();
    }

    public void OnDestroy()
    {
      controls.Player.Disable();
    }
}
