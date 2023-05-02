using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class FireballAction : MonoBehaviour
{
    
    public Transform firePoint;
    
    public Rigidbody projectilePrefab;

    public ParticleSystem smokeParticle;

    Controls controls;
    
    private float launchForce = 1300f;
    private bool canShoot = true;
    
    


    
    public void Awake()
    {
        controls = new Controls();
        controls.Gameplay.Shoot.performed += ctx => OnShoot();
    }
    public void Update()
    {
        
    }

    public void OnShoot()
    {

        if (canShoot)
        {
            var projectileInstance = Instantiate(
                projectilePrefab,
                firePoint.position,
                firePoint.rotation);

            smokeParticle.gameObject.SetActive(true);
            projectileInstance.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);

            canShoot = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        smokeParticle.gameObject.SetActive(false);
        canShoot = true;
    }

    




}