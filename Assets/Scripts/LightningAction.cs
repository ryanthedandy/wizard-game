using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAction : MonoBehaviour
{
    

    public Rigidbody lightningPrefab;

    public Transform lightningPoint;


    Controls controls;

    private bool canShoot = true;
    //private float launchForce = 10;

    





    public void Awake()
    {
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
            var projectileInstance = Instantiate(
                lightningPrefab,
                lightningPoint.position,
                lightningPoint.rotation);

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



}
