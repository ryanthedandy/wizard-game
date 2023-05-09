using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDetails : MonoBehaviour
{
    
    public int playerID;
    public Vector3 startPos;
    public int lives = 3;
    public PlayerManager playerManager;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent < PlayerController > ();
        playerManager = FindObjectOfType<PlayerManager>();
        transform.position = startPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death Zone"))
        {
            if (playerManager.playerLives[playerID] < 0)
            { 
                Debug.Log("player " + playerID);
                playerManager.playerLives.Remove(playerID);
                Destroy(gameObject);
                playerManager.checkPlayersLeft(playerManager.playerLives);
                

            }
            FindObjectOfType<AudioManager>().Play("die");
            transform.position = startPos;
            playerManager.playerLives[playerID] -= 1;

        }
    }

    
}
