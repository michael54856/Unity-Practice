using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScene : MonoBehaviour
{
    public GameObject monster;
    public GameObject square;
    public GameObject rectangle;

    public SpriteRenderer mySprite;

    public int status = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x+10, Input.mousePosition.y-10, 30));

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(status == 1)
            {
                CreateSquare();
            }
            else if(status == 2)
            {
                CreateRectangle();
            }
            else if (status == 3)
            {
                CreateMonster();
            }

            mySprite.sprite = null;
            status = 0;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            mySprite.sprite = null;
            status = 0;
        }
    }

    public void CreateSquare()
    {
        Vector3 pos =  Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30));
        Instantiate(square, pos, Quaternion.identity);
    }

    public void CreateRectangle()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30));
        Instantiate(rectangle, pos, Quaternion.identity);
    }

    public void CreateMonster()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30));
        Instantiate(monster, pos, Quaternion.identity);
    }


}
