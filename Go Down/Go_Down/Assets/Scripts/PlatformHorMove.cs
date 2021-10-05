using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHorMove : MonoBehaviour
{
    private bool goRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
       
        if(transform.position.x < -7)
        {
            goRight = true;
        }
        if (transform.position.x > 7.27)
        {
            goRight = false;
        }

        if(goRight == true)
        {
            transform.Translate(Vector2.right * Time.fixedDeltaTime * 3.5f);
        }
        else
        {
            transform.Translate(Vector2.left * Time.fixedDeltaTime * 3.5f);
        }
    }
}
