using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{   
    // player components and such
    public Animator animator;
    private Rigidbody playerRb;
    public ParticleSystem lightningDebuffParticle;
    public ParticleSystem spawnParticle;
    public PlayerDetails playerDetails;
    public AudioSource footstep;
    public PlayerManager playerManager;
    // player variables
    public float playerSpeed = 5;
    public float fallingThreshold = -2f;
    public float gravityForce = 1.5f;
    public bool isStunned = false;
    public bool falling;
    public bool spawning = true;
    // saves input as a vector 2 from input system
    private Vector2 movementInput;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerDetails = GetComponent<PlayerDetails>();
        footstep = GetComponent<AudioSource>();
        playerManager = GameObject.Find("Player Manger").GetComponent<PlayerManager>();
        // start spawn sequence
        StartCoroutine(SpawnAnimation());
    }
    void Update()
    {
        // if game isnt over, allow player to move
        if (!playerManager.gameOver)
        {
            // check if player is falling, if he is apply double gravity
            isFalling(playerRb);
            if (isStunned && !spawning)
            {
                // if player is stunned from lightning particle, add particle effect
                lightningDebuffParticle.gameObject.SetActive(true);
            }
            else if (!isStunned)
            {
                // disable lightning stun particle and allow player to move freely
                lightningDebuffParticle.gameObject.SetActive(false);
                transform.Translate(new Vector3(-movementInput.x, 0, -movementInput.y) * playerSpeed * Time.deltaTime, Space.World);
                animateCharacter(playerRb);
            }
        }
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // use movement context to enable and disable footstep sound
        movementInput = ctx.ReadValue<Vector2>();
        footstep.enabled = true;
        if (ctx.canceled)
        {
            footstep.enabled = false;
        }
    }
    private void addExtraGavity()
    {
        // add extra gravity to player if it is detected his is falling
        playerRb.AddForce(Physics.gravity * gravityForce, ForceMode.Acceleration);
    }
    private void isFalling(Rigidbody rigidBody)
    {
        // if players rigidbody velocity is above the detection for 'falling' add extra gravity
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
        // handles animationby communicating with the animation component via sending movement input details
            animator.SetFloat("horizontal", -movementInput.x);
            animator.SetFloat("vertical", -movementInput.y);      
    }
    IEnumerator SpawnAnimation()
    {
        // handles the spawning animation and particle effects and sound, also stuns player for spawn animation
        isStunned = true;
        spawnParticle.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("spawn");
        yield return new WaitForSeconds(2.9f);
        isStunned = false;
        spawning = false;
        spawnParticle.gameObject.SetActive(false);
    }
}


