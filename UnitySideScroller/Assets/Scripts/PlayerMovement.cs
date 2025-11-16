using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRB; // The Physics aspect of the player.
    public SpriteRenderer spriteRenderer; // The Graphics aspect of the player.
    public LayerMask groundLayer; // Layer mask for walkable ground.
    public LayerMask fallLayer; // Layer mask for walkable ground that can be passed through.
    public Transform bottomPosition; // Bottom of the player.
    public float movementSpeed; // Player's movement speed.
    public float input; // Value taken to determine which way the player should face.
    public float jumpStrength; // Strength of the player's jump.
    public float groundCheckCircle; // Object that is used to find what the player is standing on.
    public float activeJumpTime; // How long the player has been in the air since jumping. Increases vertical velocity up until the limited time, if the player holds the jump button.
    public float jumpTime; // The maximum amount of time the player can hold the jump button and continue to increase vertical velocity.

    private bool canJump; // Determined by whether or not the player is standing on the ground.
    private bool canFall; // Determined by whether or not the player is standing on "slippery" ground.
    private bool isJumping; // Determined by the user's input on the spacebar.
    

    // Update is called once per frame
    void Update() {
        input = Input.GetAxisRaw("Horizontal"); // "Horizontal" here, means the A and D keys, or the < and > arrow keys.
        if(input > 0) {
            spriteRenderer.flipX = true; // If moving right, look right.
        }else if(input < 0){
            spriteRenderer.flipX = false; // If moving left, look left.
        }


        canJump = Physics2D.OverlapCircle(bottomPosition.position, groundCheckCircle, groundLayer); // Is the player standing on ground?
        canFall = Physics2D.OverlapCircle(bottomPosition.position, groundCheckCircle, fallLayer); // Is the player standing on "slippery" ground?
        
        if (Input.GetButtonDown("Jump") && canJump) { // Jumps if the player presses the Spacebar and can jump.
            isJumping = true;
            activeJumpTime = jumpTime;
            playerRB.linearVelocity = Vector2.up * jumpStrength;
        }
        if(Input.GetButton("Jump") && isJumping) { // Adds more height to the jump if you hold the spacebar.
            if(activeJumpTime > 0){
                playerRB.linearVelocity = Vector2.up * jumpStrength;
                activeJumpTime -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }
        if(Input.GetButtonUp("Jump")) {
            isJumping = false;
        }
        
        if (Input.GetKeyDown(KeyCode.S) && canFall) { // Falls through the floor if you press the S key and can fall.
            StartCoroutine(FallTimer());
        }
    }

    // Fixed Update is only called 50 frames per second.
    void FixedUpdate()
    {
        // Moves the player left or right only 50 times a second to reduce the variability caused by different
        // frame rates.
        playerRB.linearVelocity = new Vector2(input * movementSpeed, playerRB.linearVelocity.y);
    }
    
    IEnumerator FallTimer() { // Disables the player's collision box for a fraction of time when called.
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
