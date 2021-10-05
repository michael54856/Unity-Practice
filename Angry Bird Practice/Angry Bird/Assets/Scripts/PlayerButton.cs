using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool draging = false;
    public GameObject playerBall;

    public Transform origin;
    public LineRenderer myLine;
    private Vector2 dir;

    public GameObject[] predictedPoints;
    public GameObject pointPrefab;

    private float predictedFrequency = 0.1f;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("gameManager");
        origin = GameObject.Find("origin").transform;
        myLine = GameObject.Find("Line").GetComponent<LineRenderer>();

        predictedPoints = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            predictedPoints[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(draging == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30));
            Vector2 originPos = origin.position;
            dir = Vector2.ClampMagnitude(mousePos - originPos, 2);
            playerBall.transform.position = origin.position + new Vector3(dir.x, dir.y, 0);
            myLine.SetPosition(0, Vector3.zero);
            myLine.SetPosition(1, (playerBall.transform.position-origin.position));

            DrawPrecdictedLine();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        playerBall.GetComponent<Rigidbody2D>().gravityScale = 0;
        myLine.positionCount = 2;
        draging = true;


    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //release
        playerBall.GetComponent<Rigidbody2D>().gravityScale = 1;
        playerBall.GetComponent<Rigidbody2D>().AddForce(dir * -350);
        myLine.positionCount = 0;
        for (int i = 0; i < 15; i++)
        {
            predictedPoints[i].transform.position = new Vector3(1000,1000,0);
        }
        draging = false;
        gameManager.GetComponent<MyGameManager>().CreatePlayerBall();
        gameObject.GetComponent<PlayerButton>().enabled = false;
    }

    public void DrawPrecdictedLine()
    {
        //(dir*-350).x * 0.02 = speed.x
        //(dir*-350).y * 0.02 = speed.y

        Vector2 v0 = (dir * -350) * 0.02f;
        Vector2 pos0 = playerBall.transform.position;
        Vector2 a = new Vector2(0, -9.81f);


        for (int i = 0; i < 15; i++)
        {
            Vector2 newPos = pos0 + (v0 * predictedFrequency*i) + ((a * (predictedFrequency * i) * (predictedFrequency * i)) / 2.0f);
            predictedPoints[i].transform.position = new Vector3(newPos.x, newPos.y, 0);
        }

    }
}
