using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightningInteraction : MonoBehaviour
{
    // List for playerscripts for edgecase that many players get stunned by same lightning
    public List<PlayerController> playerScripts = new List<PlayerController>();
    public ParticleSystem lightningParticles;
    // variables for start,end, and duration for size Lerp for the lightning projectile
    private float counter; 
    Vector3 startSize = new Vector3(.5f,.5f,.5f);
    Vector3 endSize = new Vector3(3, 3, 3);
    void Awake()
    { 
        // start lightning destruction without destroy script, so that all players get unstunned before destruction
        StartCoroutine(destroyLightning());     
    }
    void Update()
    {
        // increase size of lightning slowly over time, COOL effect
        counter += Time.deltaTime;
        transform.localScale = Vector3.Lerp(startSize, endSize, counter);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Play stun noise, add player into list, stun all players in list
            FindObjectOfType<AudioManager>().Play("stunned");
            playerScripts.Add(other.gameObject.GetComponent<PlayerController>());
            foreach(PlayerController script in playerScripts)
            {            
                script.isStunned = true;
            }
        }
    }
    public IEnumerator destroyLightning()
    {
        // start destruction of lightning projectile, first unstun all players THEN destroy
        yield return new WaitForSeconds(2);
        foreach (PlayerController script in playerScripts)
        {
            script.isStunned = false;
        }
        Destroy(gameObject);
    }
}
