using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
   public void StartGame()
    {
        // attach function to "enter the pit" button to start game, then change scene to "Game"
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        // attach to "exit" button to quit the application
        Application.Quit();
    }  
}
