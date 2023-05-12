using UnityEngine;
public class PlayerDetails : MonoBehaviour
{
    public PlayerManager playerManager;
    // player manager handles assigning the startPos and Player ID
    public Vector3 startPos;
    public int playerID; 
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        // player manager script sets startPos to a spawn point location
        transform.position = startPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        // handles player falling into the death zone
        if (other.gameObject.CompareTag("Death Zone"))
        {
            if (playerManager.playerLives[playerID] <= 0)
            {             
                // check if player is out of lives, destroy players object and start game over sequence via 'checkPlayersLeft' function
                playerManager.playerLives.Remove(playerID);
                Destroy(gameObject);
                playerManager.checkPlayersLeft(playerManager.playerLives);

            }
            else if (playerManager.playerLives[playerID] > 0)
            {
                // if player has lives left, send him back to spawn point and remove a life from his life pool
                FindObjectOfType<AudioManager>().Play("die");
                transform.position = startPos;
                playerManager.RemoveLife(playerID);
            }
        }
    }  
}
