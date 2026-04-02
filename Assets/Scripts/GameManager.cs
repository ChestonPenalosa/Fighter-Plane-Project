using UnityEngine;

public class GameManager : MonoBehaviour
{
    //enemies spawned in/instantiate
    //almost random where
    //regular spawning
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject enemyThreePrefab;
    
    void Start()
    {
        //name of function as string, delay, interval
        InvokeRepeating("CreateEnemyOne", 1f, 2f);

        InvokeRepeating("CreateEnemyTwo", 1f, 4f);

        InvokeRepeating("CreateEnemyThree", 1f, 1f);
    }

    
    void CreateEnemyOne()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f,9f), 6.5f, 0f), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-9f,9f), 6.5f, 0f), Quaternion.identity);
    }

    void CreateEnemyThree()
    {
        //1. object, 2. location, 3. rotation
        Instantiate(enemyThreePrefab, new Vector3(Random.Range(-9f,9f), 6.5f, 0f), Quaternion.identity);
    }

}
