using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform translate - movement w/o physics [direction (0,1,0), time, speed (8f)]
        //all floats need f by it if its the number
        transform.Translate(new Vector3 (0,1,0) * Time.deltaTime * 8f);

        //when bullet is high enough, detroy it
        //if statements check things - if true, code in block works; if false, code in block is ignored
        if(transform.position.y > 6.5f) //transform position = 8, code works; 6, code ignored
        {
            Destroy(this.gameObject);

        }
    }
}
