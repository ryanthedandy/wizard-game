using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TeleportAction : MonoBehaviour
{  
    public ParticleSystem teleportParticle;
    public Animator animator;

    bool canTele = true;
    public float teleDistance = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();     
    }

    IEnumerator Cooldown()
    {   
        // tele, then make player wait for cooldown to come back up
        canTele = false;
        yield return new WaitForSeconds(1);
        teleportParticle.gameObject.SetActive(false);
        canTele = true;        
    }
    public void OnTeleport(CallbackContext ctx)
    {
        if (canTele)
        {
            // start animation, then add coroutine to sync the animation movement and ability
            animator.SetTrigger("castTele");
            StartCoroutine(TeleDelay());          
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator TeleDelay()
    {
        // delay ability to sync with animation, create particle effect, then play sound
        yield return new WaitForSeconds(.55f);
        teleportParticle.gameObject.SetActive(true);
        transform.position += transform.forward * teleDistance;
        FindObjectOfType<AudioManager>().Play("tele");
    }

}
