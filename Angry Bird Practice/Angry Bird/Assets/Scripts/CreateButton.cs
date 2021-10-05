using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButton : MonoBehaviour
{
    public GameObject sceneManager;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSquare()
    {
        sceneManager.GetComponent<CreateScene>().status = 1;
        sceneManager.GetComponent<CreateScene>().mySprite.sprite = Resources.Load<Sprite>("square");
    }

    public void SetRectangle()
    {
        sceneManager.GetComponent<CreateScene>().status = 2;
        sceneManager.GetComponent<CreateScene>().mySprite.sprite = Resources.Load<Sprite>("rectangle");
    }

   public  void SetMonster()
    {
        sceneManager.GetComponent<CreateScene>().status = 3;
        sceneManager.GetComponent<CreateScene>().mySprite.sprite = Resources.Load<Sprite>("monster");
    }
}
