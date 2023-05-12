using UnityEngine;
public class Destroy : MonoBehaviour
{
    // destroy script to clear projectiles from screen
    public float destroyTimer = 1.5f;  
    void Start()
    {  
        // destroy projectile after certain amount of time
        Destroy(gameObject, destroyTimer);
    }   
}
