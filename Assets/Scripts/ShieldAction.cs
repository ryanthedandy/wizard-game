using System.Collections;
using UnityEngine;

public class ShieldAction : MonoBehaviour
{
    // prefabs, controls, firepoints and animations
    Controls controls;
    public GameObject shieldPrefab;
    public Transform firePoint;
    public Animator animator;

    // cooldown variable
    private bool canShield = true;
    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new Controls();
        controls.Gameplay.Shield.performed += ctx => OnShield();
    }

    public void OnShield()
    {
        if (canShield)
        {   
            // if able to shield, start animation, then delay shield cast to sync it with animation
            animator.SetTrigger("castShield");
            StartCoroutine(AnimationDelay());
            canShield = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        // cooldown function
        yield return new WaitForSeconds(2);
        canShield = true;
    }

    IEnumerator AnimationDelay()
    {
        // delay shield cast to sync with animation, then instantiate shield and play sound
        yield return new WaitForSeconds(.5f);
        Instantiate(
                shieldPrefab,
                firePoint.position,
                firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("shield");
    }
}
