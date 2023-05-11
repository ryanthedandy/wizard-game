using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    
    public int defaultLives = 3;
    public int maxRange = 4;
    public bool gameOver = false;
    public Transform[] spawnLocations;
    public Transform gameOverSpawn;
    public Dictionary<int, int> playerLives = new Dictionary<int, int>();
    List<int> numberList = new List<int>() { 0, 1, 2, 3};
    public TextMeshProUGUI startText;

    public TextMeshProUGUI gameOverMenu;

    private void Start()
    {
       
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
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
            playerInput.gameObject.GetComponent<PlayerDetails>().startPos = gameOverSpawn.position;
        }
        
        
           
    }

    public void checkPlayersLeft(Dictionary<int,int> players)
    {
        if (players.Count == 1)
        {
            List<int> keyList = new List<int>(players.Keys);
            gameOver = true;
            gameOverMenu.text = "Player " + keyList[0] + " WINS";
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

    public void RemoveLife(int iD)
    {
        playerLives[iD] -= 1;
    }

    



}
