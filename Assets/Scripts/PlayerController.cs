using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public bool gameOver = false;
    public float playerSpeed = 5;
    public bool isStunned = false;
    public bool falling;
    public float fallingThreshold = -2f;
    public float gravityForce = 1.5f;

    //animation bools
    public Animator animator;

    private Vector2 movementInput;
    private Rigidbody playerRb;

    public ParticleSystem lightningDebuffParticle;
    public ParticleSystem spawnParticle;

    public PlayerDetails playerDetails;

    Controls controls;

    public bool spawning = true;

    public AudioSource footstep;




    
    

 

    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerDetails = GetComponent<PlayerDetails>();
        footstep = GetComponent<AudioSource>();
        gameOver = false;
        

        StartCoroutine(SpawnAnimation());


    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {


            isFalling(playerRb);

            if (isStunned && !spawning)
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

    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();

        footstep.enabled = true;

        if (ctx.canceled)
        {
            footstep.enabled = false;
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
        
            animator.SetFloat("horizontal", -movementInput.x);
            animator.SetFloat("vertical", -movementInput.y);
        
    }

    IEnumerator SpawnAnimation()
    {
        isStunned = true;
        spawnParticle.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("spawn");
        yield return new WaitForSeconds(2.9f);
        isStunned = false;
        spawning = false;
        spawnParticle.gameObject.SetActive(false);
    }

  











}


