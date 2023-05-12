using UnityEngine;

public class ShieldInteraction : MonoBehaviour
{
    // rebound force for the wall to reflect fireballs
    public float rebound = 10000;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            // if projectile hits wall, send it back in the direction it came
            Rigidbody projectileRb = other.gameObject.GetComponent<Rigidbody>();
            projectileRb.AddForce(-other.transform.forward * rebound, ForceMode.Impulse);
        }
    }
}
