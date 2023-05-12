using System.Collections;
using UnityEngine;
public class LightningAction : MonoBehaviour
{
    // using a firepoint and lightning point so that the rotation is current, independent from the fireball
    public Rigidbody lightningPrefab;
    public Transform lightningPoint;
    public Transform firePoint;
    public Animator animator;
    Controls controls;
    // cooldown variables and launch force
    private bool canShoot = true;
    private float launchForce = 6;
    public void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new Controls();
        controls.Gameplay.Shoot.performed += ctx => OnLightning();
    }
    public void OnLightning()
    {
        // animation starts, delay cast to sync with animation
        if (canShoot)
        {
            animator.SetTrigger("castLightning");
            StartCoroutine(AnimationDelay());
            canShoot = false;
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        // cooldown function
        yield return new WaitForSeconds(2);
        canShoot = true;
    }
    IEnumerator AnimationDelay()
    {
        // delay cast to sync with animation, instantiate projectile and then play sound
        yield return new WaitForSeconds(.6f);
        var lightningInstance = Instantiate(
                lightningPrefab,
                lightningPoint.position,
                lightningPoint.rotation);
        FindObjectOfType<AudioManager>().Play("lightning");
        lightningInstance.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);      
    }
}
