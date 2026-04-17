using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //enemies spawned in/instantiate
    //almost random where
    //regular spawning
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyThreePrefab;
    public GameObject cloudPrefab;
    public GameObject gameOverMenu;
    public GameObject powerupPrefab;
    public GameObject coinPrefab;
    public GameObject lifePrefab;

    public GameObject audioPlayer;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public AudioClip coinSound;
    public AudioClip lifeSound;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI scoreText;

    private bool gameOver;
    
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        CreateSky();

        //name of function as string, delay, interval
        InvokeRepeating("CreateEnemyOne", 1f, 2f);

        InvokeRepeating("CreateEnemyTwo", 1f, 4f);

        InvokeRepeating("CreateEnemyThree", 1f, 1f);

        InvokeRepeating("CreateCoin", 1f, 8f);

        InvokeRepeating("CreateLife", 6f, 12f);

        StartCoroutine(SpawnPowerup());

        gameOver = false;

        powerupText.text = "No Power Ups Active";                
    }

    void CreateSky()
    {
        for(int i = 0; i< 30; i++)
        {
            //X random between horizontal positive and negative
            //y random between vertical negative and positive
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize,horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }
    
    void CreateEnemyOne()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize,horizontalScreenSize) *.9f, verticalScreenSize, 0f), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize,horizontalScreenSize) *.9f, verticalScreenSize, 0f), Quaternion.identity);
    }

    void CreateEnemyThree()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(enemyThreePrefab, new Vector3(Random.Range(-horizontalScreenSize,horizontalScreenSize) *.9f, verticalScreenSize, 0f), Quaternion.identity);
    }

    void CreateCoin()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(coinPrefab, new Vector3(-horizontalScreenSize * 1.2f, Random.Range(-4.5f * .9f, 0.5f * .9f), 0f), Quaternion.identity);
    }
    void CreateLife()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(lifePrefab, new Vector3(horizontalScreenSize * 1.2f, Random.Range(-4.5f * .9f, 0.5f * .9f), 0f), Quaternion.identity);
    }

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "Lives: " + currentLives; //"Lives: #"
    }

    public void ChangeScoreText(int currentScore)
    {
        scoreText.text = "Score: " + currentScore; //"Score: #"
    }

    void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GameOver()
    {
        //set our game over object menu to true
        gameOverMenu.SetActive(true);
        //game over to be true
        gameOver = true;

    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * .8f, horizontalScreenSize * .8f), Random.Range(-4.5f * .8f, 0.5f * .8f), 0), Quaternion.identity);
    }

    public void ManagerPowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No Powerups Active";
                break;
        }
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
            case 3:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(coinSound);
                break;
            case 4:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(lifeSound);
                break;
        }
    }

}
