using UnityEngine;
using System.Collections;

public class FireballAction : MonoBehaviour
{
    public Transform firePoint;
    public Rigidbody projectilePrefab;
    public Animator animator;
    public GameObject smokePrefab;
    Controls controls;

    [SerializeField]
    private float launchForce = 1500f;
    private bool canShoot = true;

    public void Awake()
    {
        // get components and send context to controls
        animator = GetComponent<Animator>();
        controls = new Controls();
        controls.Gameplay.Shoot.performed += ctx => OnShoot();
    }
    public void OnShoot()
    {
        // if able to shoot, start animation. Add coroutine for the delay to match fireball with animation, then set to false to add cooldown
        if (canShoot)
        {
            animator.SetTrigger("castFire");
            StartCoroutine(AnimationDelay());
            canShoot = false;
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        // add cooldown for spell
        yield return new WaitForSeconds(2);
        canShoot = true;
    }
    IEnumerator AnimationDelay()
    {
        // Delay fireball cast to sync with animation, then play fireball sound, instantiate smoke and projectile.
        // add force to fireball 
        yield return new WaitForSeconds(.4f);
        FindObjectOfType<AudioManager>().Play("fireball");

        var projectileInstance = Instantiate(
                projectilePrefab,
                firePoint.position,
                firePoint.rotation);

        var smokeInstance = Instantiate(
                smokePrefab,
                firePoint.position,
                firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
    }
}