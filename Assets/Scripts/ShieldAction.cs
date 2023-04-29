using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAction : MonoBehaviour
{
    Controls controls;
    public GameObject shieldPrefab;
    public Transform firePoint;
    private bool canShield = true;
    

    void Awake()
    {
        controls = new Controls();
        controls.Gameplay.Shield.performed += ctx => OnShield();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShield()
    {
        if (canShield)
        {
            var projectileInstance = Instantiate(
                shieldPrefab,
                firePoint.position,
                firePoint.rotation);

            canShield = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        canShield = true;
    }

 
}
