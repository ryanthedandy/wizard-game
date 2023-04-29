using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 5;
    public float knockBack = 500;
    
    private Vector2 movementInput;
    private Rigidbody playerRb;

 

    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // move player if axis information responds
        transform.Translate(new Vector3(-movementInput.x, 0, -movementInput.y) * playerSpeed * Time.deltaTime, Space.World);

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            playerRb.AddForce(other.gameObject.transform.forward * knockBack, ForceMode.Impulse);
        }
    }







}


