using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TeleportAction : MonoBehaviour
{
    //Controls controls;
   // Rigidbody playerRb;
    bool canTele = true;
    
    public float teleDistance = 5.0f;
    public ParticleSystem teleportParticle;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //controls = new Controls();
        //playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }

    
    /*public void Teleport()
    {
        if (canTele)
        {
            teleportParticle.gameObject.SetActive(true);
            transform.position += transform.forward * teleDistance;
            animator.SetTrigger("castTele");
            teleDistance = 0;
            StartCoroutine(Cooldown());
        }
    }
    */

    IEnumerator Cooldown()
    {
        
        canTele = false;
        yield return new WaitForSeconds(1);
        teleportParticle.gameObject.SetActive(false);
        canTele = true;
        
        
    }

    public void OnTeleport(CallbackContext ctx)
    {

        if (canTele)
        {
            animator.SetTrigger("castTele");
            StartCoroutine(TeleDelay());
            
           
            
            StartCoroutine(Cooldown());
        }


    }

    IEnumerator TeleDelay()
    {
        yield return new WaitForSeconds(.55f);
        teleportParticle.gameObject.SetActive(true);
        transform.position += transform.forward * teleDistance;
        FindObjectOfType<AudioManager>().Play("tele");
    }



}
