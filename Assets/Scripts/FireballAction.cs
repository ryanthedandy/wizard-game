using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class FireballAction : MonoBehaviour
{

    public Transform firePoint;
    public Rigidbody projectilePrefab;
    public Animator animator;
    public GameObject smokePrefab;
    Controls controls;
    private float launchForce = 1500f;
    private bool canShoot = true;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new Controls();
        controls.Gameplay.Shoot.performed += ctx => OnShoot();
    }

    public void OnShoot()
    {

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
        yield return new WaitForSeconds(2);
        canShoot = true;
    }

    IEnumerator AnimationDelay()
    {
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