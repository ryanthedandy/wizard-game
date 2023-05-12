using UnityEngine;
public class FireballInteraction : MonoBehaviour
{
    public float knockBack = 10;
    public bool isRebounding = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();
            if (!isRebounding)
            {
                playerRb.AddForce(transform.forward * knockBack, ForceMode.Impulse);
                FindObjectOfType<AudioManager>().Play("hit");
                Destroy(gameObject);
            }
            else
            {
                // if fireball is a rebounder, it changes how force is applied so that player gets sent in correct direction
                playerRb.AddForce(-transform.forward * knockBack, ForceMode.Impulse);
                FindObjectOfType<AudioManager>().Play("hit");
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Shield")
        {
            // if fireball hits a shield, it is set as a rebounding shot so that it can correctly apply force to player
            isRebounding = true;
            FindObjectOfType<AudioManager>().Play("hit");       
        }
    }
}
