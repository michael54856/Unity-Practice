using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSpike : MonoBehaviour
{

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
        if (havePlayer == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                myPlayer.GetComponent<PlayerController>().Damage(1000000);
                timer = 0.1f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "player")
        {
            havePlayer = true;
        }

    }
    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "player")
        {
            havePlayer = false;
        }
    }
}
