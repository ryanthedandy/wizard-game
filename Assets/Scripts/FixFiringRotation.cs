using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixFiringRotation : MonoBehaviour
{
    public GameObject player;
    public Rigidbody playerRb;

    private void Awake()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.rotation = playerRb.transform.rotation;
    }
}
