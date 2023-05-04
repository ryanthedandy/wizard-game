using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldAction : MonoBehaviour
{
    Controls controls;
    public GameObject shieldPrefab;
    public Transform firePoint;
    private bool canShield = true;
    public Animator animator;
    

    void Awake()
    {
        animator = GetComponent<Animator>();
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
            
            animator.SetTrigger("castShield");
            StartCoroutine(AnimationDelay());
            canShield = false;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        canShield = true;
    }

    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(
                shieldPrefab,
                firePoint.position,
                firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("shield");

    }


}
