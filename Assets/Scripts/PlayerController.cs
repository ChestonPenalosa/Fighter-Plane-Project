using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //3 things - movement, shooting, teleporting
    //teleporting and movement together

    //function that gets called ONLY when you press space (intermediate class)

    public GameObject bulletPrefab;
    public GameObject bulletTwoPrefab;
    public GameObject explosionPrefab;

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    //private float horizontalScreenLimit = 9.5f;
    //private float verticalScreenLimit = 6.5f;
    private float topScreenLimit = 0.5f;
    private float bottomScreenLimit = -4.5f;

    public int lives;
    public GameManager gameManager;
       
    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
        
    }

    void Update()
    {
        Shooting();
        Movement();

    }

    public void LoseALife()
    {
        lives--;
        gameManager.ChangeLivesText(lives);
        if(lives ==0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    
    void Movement()
    {
        //read WASD - "horizontal" and "vertical" axis
        horizontalInput = Input.GetAxis("Horizontal"); // Needs to be capitalized
        verticalInput = Input.GetAxis("Vertical"); // capitalized
        
        //translate takes in vector(direction) mulitplied by time and speed
        transform.Translate(new Vector3(horizontalInput,verticalInput,0) * Time.deltaTime * playerSpeed);
       
        //player leaves horizontally
        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if(transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        /*
        if(transform.position.y > verticalScreenSize || transform.position.y <= -verticalScreenSize)                
        {                        
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);               
        }
        */
        
        if(transform.position.y > topScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, bottomScreenLimit, 0);
        }
        if(transform.position.y < bottomScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, topScreenLimit, 0);
        }

    }
   
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //1 what we are spawning, position we spawn it at, rotation we spawn it at
            Instantiate(bulletPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);

        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //1 what we are spawning, position we spawn it at, rotation we spawn it at
            Instantiate(bulletTwoPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);

        }

    }
}
