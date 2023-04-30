using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningInteraction : MonoBehaviour
{

    public List<PlayerController> playerScripts = new List<PlayerController>();
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("It hits");
            playerScripts.Add(other.gameObject.GetComponent<PlayerController>());
            foreach(PlayerController script in playerScripts)
            {
                script.isStunned = true;
            }
            StartCoroutine(destroyLightning());

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
