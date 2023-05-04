using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
    

public class PlayerManager : MonoBehaviour
{
    public Transform[] spawnLocations;

    // use this list to make spawn locations random, and prevent double spawn
    List<int> spawnIndexList = new List<int>()
    {
        0,
        1,
        2,
        3,
    };
    
    



    public void OnPlayerJoin(PlayerInput playerInput)
    {
        Debug.Log(playerInput.playerIndex);
        // Set the player ID, add one to the index to start at Player 1
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;

        int spawnIndex = spawnIndexList[Random.Range(0, 3)];

        // Set the start spawn position of the player using the location at the associated element into the array.
        playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[spawnIndex].position;

        // remove chance of double spawn
        spawnIndexList.Remove(spawnIndex);

        if (spawnLocations[spawnIndex] == null)
        {
            spawnIndexList.Add(0);
            spawnIndexList.Add(1);
            spawnIndexList.Add(2);
            spawnIndexList.Add(3);

        }
    }



}
