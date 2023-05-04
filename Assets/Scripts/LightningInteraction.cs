using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningInteraction : MonoBehaviour
{

    public List<PlayerController> playerScripts = new List<PlayerController>();
    private float counter;
    //private float duration = 20.0f;
    public ParticleSystem lightningParticles;
    Vector3 startSize = new Vector3(.5f,.5f,.5f);
    Vector3 endSize = new Vector3(3, 3, 3);
    void Awake()
    {
        
        StartCoroutine(destroyLightning());
        

       
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        transform.localScale = Vector3.Lerp(startSize, endSize, counter);
       
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
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
        yield return new WaitForSeconds(2);
        foreach (PlayerController script in playerScripts)
        {
            script.isStunned = false;
        }
        Destroy(gameObject);
    }

    



}
