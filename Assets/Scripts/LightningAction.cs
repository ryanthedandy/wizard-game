using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAction : MonoBehaviour
{
    

    public Rigidbody lightningPrefab;

    public Transform lightningPoint;
    public Transform firePoint;
    public Animator animator;


    Controls controls;

    private bool canShoot = true;
    private float launchForce = 2;
    

    





    public void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new Controls();
        controls.Gameplay.Shoot.performed += ctx => OnLightning();
       

    }
    public void Update()
    {

    }

    public void OnLightning()
    {

        if (canShoot)
        {
            animator.SetTrigger("castLightning");
            StartCoroutine(AnimationDelay());

           // var projectileInstance = 
                
                
              
            //projectileInstance.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
           
            canShoot = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        canShoot = true;
    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(.6f);
        var lightningInstance = Instantiate(
                lightningPrefab,
                lightningPoint.position,
                lightningPoint.rotation);
        FindObjectOfType<AudioManager>().Play("lightning");
        lightningInstance.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
        
        

    }



}
