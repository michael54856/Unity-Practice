using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject myESC;

    public GameObject player;
    public Transform playerSpawn;
    public Transform originSpawn;

    public GameObject origin;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTheGame()
    {
        Time.timeScale = 1;
        myESC.SetActive(true);

        player.transform.position = playerSpawn.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(origin, originSpawn.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
