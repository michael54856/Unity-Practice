using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("drop", false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        anim.SetBool("drop", true);
    }
}
