using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject normal;
    public GameObject moving;
    public GameObject spike;


    // Start is called before the first frame update
    private float timer;
    void Start()
    {
        timer = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        //-6.69~7.11
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float x = Random.Range(-7f, 7.11f);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            int n = Random.Range(0, 3);
            
            if (n == 0)
            {
                Instantiate(normal, transform.position, Quaternion.identity);
            }
            else if(n == 1)
            {
                Instantiate(moving, transform.position, Quaternion.identity);
            }
            else if(n == 2)
            {
                Instantiate(spike, transform.position, Quaternion.identity);
            }
            timer = 0.75f;
        }
    }
}
