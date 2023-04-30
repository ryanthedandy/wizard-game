using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldInteraction : MonoBehaviour
{
    public float rebound = 10000;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            
            Rigidbody projectileRb = other.gameObject.GetComponent<Rigidbody>();
            projectileRb.AddForce(-other.transform.forward * rebound, ForceMode.Impulse);
        }
    }
}
