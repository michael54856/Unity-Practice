using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isSpike;
    public float timer = 0;
    public bool havePlayer = false;
    public GameObject myPlayer;
    void Start()
    {
        myPlayer = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(havePlayer == true && isSpike == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                myPlayer.GetComponent<PlayerController>().Damage(1);
                timer = 0.5f;
            }
        }
    }

    public void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.fixedDeltaTime * 2f);
        if(transform.position.y > 8)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collisionInfo) 
    {
        if(collisionInfo.gameObject.name == "player" && isSpike == true)
        {
            havePlayer = true;
        }

        if (collisionInfo.gameObject.name == "player" && isSpike == false)
        {
            myPlayer.GetComponent<PlayerController>().Heal(1);
        }

    }
    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "player" && isSpike == true)
        {
            havePlayer = false;
        }
    }
}
