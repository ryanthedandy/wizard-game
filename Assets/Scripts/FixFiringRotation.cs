using UnityEngine;
public class FixFiringRotation : MonoBehaviour
{
    // using this script on the firing point to fix a bug where collisions where it would spin wildly causing inaccurate firing.
    public GameObject player;
    public Rigidbody playerRb;
    private void Awake()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }
    void Update()
    {
        // use this to keep the firing point at the same rotation as the player
        transform.rotation = playerRb.transform.rotation;
    }
}
