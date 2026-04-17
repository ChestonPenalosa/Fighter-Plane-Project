using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //3 things - movement, shooting, teleporting
    //teleporting and movement together

    //function that gets called ONLY when you press space (intermediate class)

    public GameObject bulletPrefab;
    public GameObject bulletTwoPrefab;
    public GameObject explosionPrefab;

    public GameObject thruster;
    public GameObject shield;
    public int weaponType;
    public bool shieldActive;

    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    //private float horizontalScreenLimit = 9.5f;
    //private float verticalScreenLimit = 6.5f;
    private float topScreenLimit = 0.5f;
    private float bottomScreenLimit = -4.5f;

    public int lives;
    public int score;
    public GameManager gameManager;
       
    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        score = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
        gameManager.ChangeScoreText(score);
        shieldActive = false;
        weaponType = 1;
        
    }

    void Update()
    {
        Shooting();
        Movement();

    }

    public void LoseALife()
    {
        if (!shieldActive)
        {
            lives--;
        }
        if(shieldActive == true)
        {
            shield.SetActive(false);
            shieldActive = false;
        }
        
        gameManager.ChangeLivesText(lives);

        if(lives ==0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            gameManager.GameOver();

            Destroy(this.gameObject);
        }
    }
       
    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagerPowerupText(5);
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 6f;
        thruster.SetActive(false);
        gameManager.PlaySound(2);
        gameManager.ManagerPowerupText(5);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1;
        gameManager.PlaySound(2);
        gameManager.ManagerPowerupText(5);
    }

    //use for colliding with objects/items
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "PowerUp")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    playerSpeed = 10f;                  
                    thruster.SetActive(true);
                    gameManager.ManagerPowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    weaponType = 2;
                    gameManager.ManagerPowerupText(2);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    weaponType = 3;
                    gameManager.ManagerPowerupText(3);
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 4:
                    shield.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagerPowerupText(4);
                    StartCoroutine(ShieldPowerDown());
                    break;

            }
        }
        if (whatDidIHit.tag == "Coin")
        {
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(3);
            score++;
            gameManager.ChangeScoreText(score);

        }

        if (whatDidIHit.tag == "Life")
        {
            Destroy(whatDidIHit.gameObject);
            gameManager.PlaySound(4);
            if(lives < 3)
            {
                lives++;
                gameManager.ChangeLivesText(lives);

            }
            if(lives == 3)
            {
                score++;
                gameManager.ChangeScoreText(score);
            }

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
        
        //player leaves vertically
        if(transform.position.y > topScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, topScreenLimit, 0);
        }
        if(transform.position.y < bottomScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, bottomScreenLimit, 0);
        }

    }
   
    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            /*
            //1 what we are spawning, position we spawn it at, rotation we spawn it at
            Instantiate(bulletPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            */
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }

        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            /*
            //1 what we are spawning, position we spawn it at, rotation we spawn it at
            Instantiate(bulletTwoPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
            */
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletTwoPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletTwoPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletTwoPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletTwoPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletTwoPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletTwoPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }

        }

    }
}
