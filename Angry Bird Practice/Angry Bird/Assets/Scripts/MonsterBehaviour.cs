using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public GameObject pointsManager;
    public AudioSource audioManager;
    // Start is called before the first frame update
    void Start()
    {
        pointsManager = GameObject.Find("pointsManager");
        audioManager = GameObject.Find("audioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 5)
        {
            pointsManager.GetComponent<PointManager>().AddPoints(100);
            audioManager.Play();
            Destroy(gameObject);
        }
            
    }

}
