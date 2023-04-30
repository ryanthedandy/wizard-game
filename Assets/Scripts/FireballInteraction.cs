using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballInteraction : MonoBehaviour
{
    public float knockBack = 50;
    public bool isRebounding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Rigidbody projectileRb = other.gameObject.GetComponent<Rigidbody>();

            if (!isRebounding)
            {
                projectileRb.AddForce(transform.forward * knockBack, ForceMode.Impulse);
            }
            else
            {
                projectileRb.AddForce(-transform.forward * knockBack, ForceMode.Impulse);
            }

        }

        if (other.gameObject.tag == "Shield")
        {
            isRebounding = true;
        }
    }




}
