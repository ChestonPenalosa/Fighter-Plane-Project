using UnityEngine;

public class Glider : MonoBehaviour
{
    public bool goingUp;
    public float speed;
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if(goingUp == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if(transform.position.y < -gameManager.verticalScreenSize *1.25f || transform.position.y > gameManager.verticalScreenSize *1.25f) 
        {
            Destroy(this.gameObject);
        }
        /*
        transform.Translate(new Vector3 (0,-1,0) * Time.deltaTime * 3f);
        
        if(transform.position.y < -6.5f) 
        {
            Destroy(this.gameObject);
        }
        */
    }
}
