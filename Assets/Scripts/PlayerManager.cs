using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    // hold all spawn locations on the map
    public Transform[] spawnLocations;
    // this transform is to fix a bug of the losing player spawning 'again' after losing
    public Transform gameOverSpawn;
    // dictionary to keep track of player ID and match with player lives
    public Dictionary<int, int> playerLives = new Dictionary<int, int>();
    public TextMeshProUGUI startText;
    public TextMeshProUGUI gameOverMenu;
    // use this list to prevent two players from spawning in the same spawn point
    List<int> numberList = new List<int>() { 0, 1, 2, 3 };
    // default lives, a value to use to fix double spawns, and the gameover variable
    public int defaultLives = 3;
    public int maxRange = 4;
    public bool gameOver = false;
    public void OnPlayerJoin(PlayerInput playerInput)
    {
        // If game isnt over, add player to players dictionary, give him an ID and then assign him a spawn, then remove the spawn from the list
        // this prevents double spawns and then remove the starting "Press start" intro text
        if (!gameOver)
        {
            int iD = playerInput.playerIndex + 1;
            int index = numberList[Random.Range(0, maxRange)];
            playerInput.gameObject.GetComponent<PlayerDetails>().playerID = iD;
            playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[index].position;
            playerLives.Add(iD, defaultLives);
            numberList.Remove(index);
            maxRange -= 1;
            startText.gameObject.SetActive(false);
        }
        else if (gameOver)
        {
            // if game is already over, have fresh spawns spawn into the lava as to not disrupt the final sequence
            playerInput.gameObject.GetComponent<PlayerDetails>().startPos = gameOverSpawn.position;
        }        
    }
    public void checkPlayersLeft(Dictionary<int,int> players)
    {
        // check if only one player left
        if (players.Count == 1)
        {
            // if only 1 player,use key list to determine which player and create "player ? wins" text, set game over to true to restrict movement
            List<int> keyList = new List<int>(players.Keys);
            gameOver = true;
            gameOverMenu.text = "Player " + keyList[0] + " WINS";
            GameObject.Find("Player(Clone)").GetComponent<PlayerController>().footstep.enabled = false;
            gameOverMenu.gameObject.SetActive(true);
            // start coroutine to send players back to the menu
            StartCoroutine(gameIsOver());
        }     
    }
    IEnumerator gameIsOver()
    {
        // add delay, then send players back to Menu scene
        yield return new WaitForSeconds(5);
        gameOverMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void RemoveLife(int iD)
    {
        // function to remove a life when player falls in lava
        playerLives[iD] -= 1;
    }

    



}
