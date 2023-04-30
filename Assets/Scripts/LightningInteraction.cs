using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningInteraction : MonoBehaviour
{
    PlayerController playerScript;
    void Awake()
    {
        playerScript = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        StartCoroutine(destroyLightning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            StartCoroutine(lightningCoolDown());
            
        }
    }

    public IEnumerator lightningCoolDown()
    {
        playerScript.isStunned = true;
        yield return new WaitForSeconds(2);
        playerScript.isStunned = false;
        
        

    }

    public IEnumerator destroyLightning()
    {
        yield return new WaitForSeconds(2);
        playerScript.isStunned = false;
        Destroy(gameObject);
    }






}
