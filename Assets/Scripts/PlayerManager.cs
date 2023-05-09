using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int totalPlayers = 0;
    public int defaultLives = 3;
    public Transform[] spawnLocations;
    public Dictionary<int, int> playerLives = new Dictionary<int, int>();
    public PlayerController playerController;
    public TextMeshProUGUI gameOverMenu;

    private void Start()
    {
       
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        
        // Set the player ID, add one to the index to start at Player 1
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;     
        playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[Random.Range(0,4)].position;
        playerLives.Add(playerInput.playerIndex + 1, defaultLives);
        totalPlayers += 1;
           
    }

    public void checkPlayersLeft(Dictionary<int,int> players)
    {
        if (players.Count <= 1)
        {
            Debug.Log("ENTERS STATEMENT");
            GameObject.Find("Player(Clone)").GetComponent<PlayerController>().gameOver = true;
            gameOverMenu.text = "Player " + GameObject.Find("Player(Clone)").GetComponent<PlayerDetails>().playerID + " WINS";
            GameObject.Find("Player(Clone)").GetComponent<PlayerController>().footstep.enabled = false;
            gameOverMenu.gameObject.SetActive(true);
            StartCoroutine(gameIsOver());
        }   
       
    }

    IEnumerator gameIsOver()
    {
        yield return new WaitForSeconds(5);
        gameOverMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    



}
