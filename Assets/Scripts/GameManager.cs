using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //enemies spawned in/instantiate
    //almost random where
    //regular spawning
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyThreePrefab;
    public GameObject cloudPrefab;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public TextMeshProUGUI livesText;
    
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;

        CreateSky();

        //name of function as string, delay, interval
        InvokeRepeating("CreateEnemyOne", 1f, 2f);

        InvokeRepeating("CreateEnemyTwo", 1f, 4f);

        InvokeRepeating("CreateEnemyThree", 1f, 1f);
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

    public void ChangeLivesText (int currentLives)
    {
        livesText.text = "lives " + currentLives; //"lives #"
    }

}
