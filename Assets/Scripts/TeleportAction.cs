using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TeleportAction : MonoBehaviour
{
    //Controls controls;
   // Rigidbody playerRb;
    bool canTele = true;
    bool held = false;
    public float teleDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        //controls = new Controls();
        //playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            if (teleDistance < 5.0f)
            {
                teleDistance += 5.0f;
            }
        }
 
    }

    
    public void Teleport()
    {
        if (canTele)
        {
            
            transform.position += transform.forward * teleDistance;
            teleDistance = 0;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        
        canTele = false;
        yield return new WaitForSeconds(1);
        canTele = true;
        
        
    }

    public void OnTeleport(CallbackContext ctx)
    {
        if (canTele)
        {
            if (ctx.performed)
                held = true;
            if (ctx.canceled)
            {
                held = false;
                Teleport();
            }
        }
    }


}
