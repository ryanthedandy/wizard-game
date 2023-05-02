using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed = 5;
    public bool isStunned = false;
    public bool falling;
    public float fallingThreshold = -2f;
    public float gravityForce = 1.5f;

    //animation bools
    public Animator animator;
    public Vector2 inputVector;



    private Vector2 movementInput;
    private Rigidbody playerRb;

    public ParticleSystem lightningDebuffParticle;
    
    

 

    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal"); 
        inputVector.y = Input.GetAxis("Vertical");

        isFalling(playerRb);
        if (isStunned)
        {
            lightningDebuffParticle.gameObject.SetActive(true);
        } 
        else if (!isStunned)
        {
            lightningDebuffParticle.gameObject.SetActive(false);
            transform.Translate(new Vector3(-movementInput.x, 0, -movementInput.y) * playerSpeed * Time.deltaTime, Space.World);
        }

        
        animateCharacter(playerRb);

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death Zone"))
        {
            Destroy(gameObject);
        }
    }

    private void addExtraGavity()
    {
        playerRb.AddForce(Physics.gravity * gravityForce, ForceMode.Acceleration);
    }

    private void isFalling(Rigidbody rigidBody)
    {
        if (rigidBody.velocity.y < fallingThreshold)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }

        if (falling)
        {
            addExtraGavity();
        }
    }


    public void animateCharacter(Rigidbody rigidbody)
    {
        animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxis("Vertical"));

    }











}


