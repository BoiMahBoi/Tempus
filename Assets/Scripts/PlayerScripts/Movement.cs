using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public class Movement : MonoBehaviour
{
    //                                  !!! ATTENTION !!!
    // TO START A ROUND AND MOVE THE CHARACTER PRESS "P" EVERY TIME YOU WANT TO START A NEW ROUND
    // IF YOU WANT TO RESET THE LEVEL PRESS "R" - IT DELETES ALL NEW PLAYERS AND RESETS PLAYER VALUES

    [SerializeField] GameObject playerPrefab; //Reference to the player gameobject, used to instantiate new player clones
    private LevelResetManager levelResetManager;

    [SerializeField] private float moveSpeed = 5f; //The movement speed of the player
    [SerializeField] private float jumpHeight = 3.5f; //The jump height in tiles

    private float jumpInput; //Input used to make the player jump
    private float movementInput; //Input used to actually move the players
    private float currentMovement; //Current input used to move player
    private float lastMovement; //Value for last input used to control player, Used to check if a new input is used.

    [SerializeField] private LayerMask tilemapLayer; //Reference to layer tilemap is on, and other objects the player should collide with
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private Transform groundCheck; //Empty Object with transform located at the bottom middle of Gameobject
    [SerializeField] private Transform wallCheckRight; //Empty Object with transform located at the rightside middle of Gameobject
    [SerializeField] private Transform wallCheckLeft; //Empty Object with transform located at the leftside middle of Gameobject

    private float currentTime; //The current time for the round being played
    [SerializeField] private float levelTimer; //The time in seconds, before the round ends
    private float roundStartTime; //The current time, when the round starts, used to have a round clock
    public float roundNumber = 1f; //Number representing the current round in the level

    private float boxCastLenght = 0.2f; //Value used in boxcasting to help position and size 
    private Vector2 initialPosition; //The initial position of the player, when the level starts
    private bool isGrounded; //Boolean for when player is grounded, to avoid in-air jumps
    [SerializeField] private bool isWalled; //Boolean for when player is touching a wall, to avoid getting stcuk on walls
    //[SerializeField] private bool isFalling;
    private bool isPlaying; //Boolean that determines the current state, pausing the players when false
    private bool CallStartNewRound; //Boolean used to start round in fixed update, to avoid timer running on weird values
    private Rigidbody2D rb;
    private Animator animator;
    private GameObject beginText;
    public bool canPlay;

    //Float array lists storing the values for movement, jump and current time, used to move players
    public List<float[]> listJump = new List<float[]>();
    public List<float[]> listMovement = new List<float[]>();

    private void Awake()
    {
        //Getting reference to rigidbody
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        beginText = GameObject.Find("BeginText");
    }

    void Start()
    {
        levelResetManager = GameObject.Find("GameManager").GetComponent<LevelResetManager>();

        //Initialposition is set when object is ready
        initialPosition = transform.position;
        animator.Play("TimeRift");
    }

    void ResetRound()
    {
        //TO CHECK FOR DEVIATION IN MOVEMENT, SEACH [CASE X] IN CONSOLE - MIND YOU, THIS ONLY WORKS IN STATIC LEVELS WITH NO DYNAMIC PLATFORMS
        Debug.Log(gameObject.name + " moved to " + transform.position + "  [Case " + roundNumber + "]"); //Debugging for movement error .. 

        ResetPlayers();
        levelResetManager.resetObjectState();
        InstantiateNewPlayer();
        animator.Play("TimeRift");
        beginText.GetComponent<TextMeshProUGUI>().SetText("Press P to begin loop");
    }

    void ResetLevel()
    {
        if (gameObject.name != "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            roundNumber = 1;
            ResetPlayers();
            ClearLists();
        }
    }

    void ResetPlayers()
    {
        isPlaying = false; //Setting isPlaying to false, pausing the players
        movementInput = 0f; //Setting input to 0, for next round to combat unwanted movement
        jumpInput = 0f; //Setting input to 0, for next round to combat unwanted movement
        transform.position = initialPosition; //Setting player position back to initial position
        rb.velocity = Vector2.zero; rb.angularVelocity = 0f; //Setting velocity to 0, to fix a jump bug occuring right after reset and prevent what else might happen
        gameObject.GetComponent<SpriteRenderer>().flipX = false; //Set the player facing towrds the right
    }

    void InstantiateNewPlayer()
    {
        if (gameObject.name == "Player") //Checks if the gameobject is the OG player

        {
            GameObject newPlayer = Instantiate(playerPrefab); //Instantiating a new player using the player prefab
            newPlayer.name = "Player " + roundNumber; //Naming the new player object the same number as the before-played round
            roundNumber++; //Incrementing the number of rounds by one
            newPlayer.transform.position = initialPosition; //Setting the new players position equal to the initial position
            newPlayer.GetComponent<SpriteRenderer>().color  = new Color(1, 1, 1, 0.5f); ; //Colouring the new player a random colour using spriterenderer component
            newPlayer.GetComponent<SpriteRenderer>().sortingOrder = 0;
            newPlayer.transform.localScale = Vector3.one;

            //Setting the range of the new player movement and jump list equal to the current ones, so that they can hold the same values
            newPlayer.GetComponent<Movement>().listJump.AddRange(listJump);
            newPlayer.GetComponent<Movement>().listMovement.AddRange(listMovement);

            //For loop iterating through the elements of the jump list setting the values of the new player list to the current list
            for (int i = 0; i < listJump.Count; i++)
            {
                newPlayer.GetComponent<Movement>().listJump[i][0] = listJump[i][0];
                newPlayer.GetComponent<Movement>().listJump[i][1] = listJump[i][1];
            }

            //For loop iterating through the elements of the movement list setting the values of the new player list to the current list
            for (int i = 0; i < listMovement.Count; i++)
            {
                newPlayer.GetComponent<Movement>().listMovement[i][0] = listMovement[i][0];
                newPlayer.GetComponent<Movement>().listMovement[i][1] = listMovement[i][1];
            }

            ClearLists();
        }

    }

    void ClearLists()
    {
        //Clearing list holding movement inputs for the Player, so that it can record new movements
        listJump.Clear();
        listMovement.Clear();
    }

    void RecordMovement() //Function for when new movement value should be stored with current time
    {
        float[] newInput = new float[2];
        newInput[0] = currentTime;
        newInput[1] = currentMovement;
        listMovement.Add(newInput);
    }

    void RecordJump() //Function for when new jump value should be stored with current time
    {
        float[] newInput = new float[2];
        newInput[0] = currentTime;
        newInput[1] = jumpInput;
        listJump.Add(newInput);
    }

    void PlayMovement() //Function for when movement value should we set equal to stored movement
    {
        for (int i = 0; i < listMovement.Count; i++)
        {
            if (currentTime == listMovement[i][0])
            {
                movementInput = listMovement[i][1];
                if (isGrounded)
                {
                    CreateDust();
                }
            }
        }
    }

    void PlayJump() //Function for when jump value should we set equal to stored movement
    {
        for (int i = 0; i < listJump.Count; i++)
        {
            if (currentTime == listJump[i][0])
            {
                jumpInput = listJump[i][1];
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    void Update()
    {
        if (gameObject.name == "Player") //Only allowed by OG player
        {
            if (isGrounded && Input.GetButtonDown("Jump") && isPlaying) //Checks is the player is grounded, input jump is true and if the round is playing
            {
                jumpInput = 1f; //Sets the jump input to 1 AKA true
                RecordJump(); //Calls function to store new jump value
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && !isPlaying && canPlay) //Checks if the round is not playing, and if P is pressed
        {
            beginText.GetComponent<TextMeshProUGUI>().SetText("Press R to restart loop");
            CallStartNewRound = true; //Boolean used to call StartNewRound() in fixedupdate, since it SHAN'T be called in update, ruining the whole mechanic
        }

        if (Input.GetKeyDown(KeyCode.R)) //Checks if the round is not playing, and if P is pressed
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //Checks if the round is not playing, and if P is pressed
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void FixedUpdate()
    {
        if (CallStartNewRound) //Checks if boolean is true
        {
            StartNewRound(); //Calls function in fixedupdate

            CallStartNewRound = false; //Sets boolean to false, so that it doesn't loop and kill the game
        }

        void StartNewRound() //Function that starts a new round by
        {
            roundStartTime = Time.time; //Setting the start time to Time.time which is the same as setting the round to 0, since the timer counts up
            isPlaying = true; //Sets the isPlaying boolean to true, allowing most of the code to work such as input, movement etc.
        }

        // Calculate the size and position of the box cast
        Vector2 groundBoxSize = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, boxCastLenght);
        Vector2 groundBoxPosition = new Vector2(groundCheck.position.x, groundCheck.position.y);


        // Check if player is grounded using a box cast
        RaycastHit2D groundHit = Physics2D.BoxCast(groundBoxPosition, groundBoxSize, 0f, Vector2.down, boxCastLenght, tilemapLayer);
        isGrounded = (groundHit.collider != null);

        //Checks if a round is currently being played
        if (isPlaying)
        {
            //Updating current time and reducing current time to 2 decimal places, to make sure all movement inputs gets used
            currentTime = Mathf.Round((Time.time - roundStartTime) * 100f) / 100f;

            if (currentTime >= levelTimer)  //When the time passed in the round is equal or as a precaution exceeds the leveltimer
            {
                ResetRound(); //Calls the function that resets the round
            }

            currentMovement = Input.GetAxisRaw("Horizontal"); //Using the Unity input system, seeting the current movement equal to the current input on horizontal axis

            if (!isWalled) //Checks if the player is touching a wall
            {
                // Moves player horizontally        
                rb.velocity = new Vector2(movementInput * moveSpeed, rb.velocity.y);
            }

            if (isGrounded && jumpInput == 1f) //Checks if the player is on the ground, and is receiving a jump input
            {
                jumpInput = 0f; //Sets the jump to 0, since that is not stored in the jump list
                // Calculate jump force based on jump height, and thereafter adding that force upwards
                float jumpForce = Mathf.Sqrt(2 * jumpHeight * Physics2D.gravity.magnitude);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
                CreateDust();
            }


            //Only player-allowed functions
            if (gameObject.name == "Player")
            {
                if (currentMovement != lastMovement)
                {
                    lastMovement = currentMovement;
                    RecordMovement();
                }
            }

            if (!isGrounded && rb.velocity.y <= 0)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                //isFalling = true;

            } else
            {
                animator.SetBool("isFalling", false);
                //isFalling = false;
            }



            //Checks if the characters movement is left or right
            if (movementInput != 0)
            {
                Vector2 wallBoxSize = new Vector2(boxCastLenght, gameObject.GetComponent<BoxCollider2D>().size.y);

                //If the characters movement input is to the right, the spriterenderer on x is set to 1 flipping the sprite to the original direction
                if (movementInput > 0)
                {

                    gameObject.GetComponent<SpriteRenderer>().flipX = false;

                    //Calculate the size and position of wall box cast
                    Vector2 wallBoxPosition = new Vector2(wallCheckRight.position.x, wallCheckRight.position.y);

                    //Check if player is walled using a box cast
                    RaycastHit2D wallHit = Physics2D.BoxCast(wallBoxPosition, wallBoxSize, 0f, Vector2.right, boxCastLenght, tilemapLayer);
                    isWalled = (wallHit.collider != null);
                }

                //If the characters movement input is to the left, the spriterenderer on x is set to -1 flipping the sprite to the opposite direction
                else if (movementInput < 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;

                    //Calculate the size and position of wall box cast
                    Vector2 wallBoxPosition = new Vector2(wallCheckLeft.position.x, wallCheckLeft.position.y);

                    //Check if player is walled using a box cast
                    RaycastHit2D wallHit = Physics2D.BoxCast(wallBoxPosition, wallBoxSize, 0f, Vector2.left, boxCastLenght, tilemapLayer);
                    isWalled = (wallHit.collider != null);
                }

                if(isGrounded && !animator.GetBool("isRunning"))
                {
                    animator.SetBool("isRunning", true);
                }
            }
            else if (movementInput == 0 && animator.GetBool("isRunning")) {
                animator.SetBool("isRunning", false);
            }
            PlayMovement(); //Calling funtion to check if new movement input is stored
            PlayJump(); //Calling funtion to check is new jump input is stored
        }

















        //Visualization of the 2 box casts made, checking for ground and wall .. this is just for debugging, has no impact on the player
        // Drawing the ground box cast using Debug.DrawLine
        Vector2 groundTopLeft = new Vector2(groundBoxPosition.x - groundBoxSize.x / 2, groundBoxPosition.y + groundBoxSize.y / 2);
        Vector2 groundTopRight = new Vector2(groundBoxPosition.x + groundBoxSize.x / 2, groundBoxPosition.y + groundBoxSize.y / 2);
        Vector2 groundBottomLeft = new Vector2(groundBoxPosition.x - groundBoxSize.x / 2, groundBoxPosition.y - groundBoxSize.y / 2);
        Vector2 groundBottomRight = new Vector2(groundBoxPosition.x + groundBoxSize.x / 2, groundBoxPosition.y - groundBoxSize.y / 2);
        Debug.DrawLine(groundTopLeft, groundTopRight);
        Debug.DrawLine(groundTopRight, groundBottomRight);
        Debug.DrawLine(groundBottomRight, groundBottomLeft);
        Debug.DrawLine(groundTopLeft, groundTopRight);

       
    }
}
