using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerMovement : NetworkBehaviour
{
    [SyncVar]public int playerID;
    public bool ready = false;
    private float moveSpeed = 10;
    public Rigidbody rb;

    public GridGenerator gridManager;
    public GameObject bomb;

    public int lastMove = 0;

    public int bombCount;
    public float speed = 1;
    public int power = 1;

    private int maxBombCount = 1;

    public Material redSkin;
    public Material yellowSkin;

    private TextMeshProUGUI bombText;
    private TextMeshProUGUI speedText;
    private TextMeshProUGUI powerText;

    public GameObject MyNetworkManagerObj;

    public Color32 myGreen = new Color32(165, 255, 180, 255);
    public Color32 myBlue = new Color32(165, 248, 255, 255);
    public Color32 myRed = new Color32(255, 150, 150, 255);

    public bool canMove = false;

    public GameObject myGoggle;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("gridManager").GetComponent<GridGenerator>();

        bombText = GameObject.Find("bombText").GetComponent<TextMeshProUGUI>();
        speedText = GameObject.Find("speedText").GetComponent<TextMeshProUGUI>();
        powerText = GameObject.Find("powerText").GetComponent<TextMeshProUGUI>();

        MyNetworkManagerObj = GameObject.Find("NetworkManager");

        if(isLocalPlayer)
        {
            if (isServer)
            {
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.text = "p1 not ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.color = myRed;
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.text = "wait for join...";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.color = myBlue;
            }
            else
            {
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.text = "p1 not ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.color = myRed;
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.text = "p2 not ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.color = myRed;
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyButtonText.text = "ready";
                PlayerJoinToServer();
            }
        }
       
    }

    [Command]
    public void ChangeReadyTextCommand(int num, bool toTrue)
    {
        ChangeReadyText(num, toTrue);

        if(toTrue == true)
        {
            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject myPlayer in playerList)
            {
                if (myPlayer != gameObject)
                {
                    if (myPlayer.GetComponent<PlayerMovement>().ready == true)//if other player is ready
                    {
                        playerList[0].GetComponent<PlayerMovement>().StartTheGame();
                        playerList[1].GetComponent<PlayerMovement>().StartMoving();
                    }
                }
            }
        }
       
    }

    [ClientRpc]
    public void StartTheGame()
    {
        canMove = true;
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyButton.SetActive(false);
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyMenu.SetActive(false);
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.gameObject.SetActive(false);
        gridManager.GetComponent<GridGenerator>().ClearScene();
        gridManager.GetComponent<GridGenerator>().CreateScene();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        myGoggle.SetActive(true);
        if (isLocalPlayer)
        {
            bombCount = 1;
            speed = 1;
            power = 1;
            maxBombCount = 1;
            powerText.text = "power:" + power.ToString();
            bombText.text = "bomb:" + maxBombCount.ToString();
            speedText.text = "speed:" + speed.ToString();

            if (playerID == 1)
            {
                transform.position = MyNetworkManagerObj.GetComponent<MyNetworkManager>().spawn_1.position;
            }
            else
            {
                transform.position = MyNetworkManagerObj.GetComponent<MyNetworkManager>().spawn_2.position;
            }
        }

    }

    [ClientRpc]
    public void StartMoving()
    {
        canMove = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        myGoggle.SetActive(true);
        if (isLocalPlayer)
        {
            bombCount = 1;
            speed = 1;
            power = 1;
            maxBombCount = 1;
            powerText.text = "power:" + power.ToString();
            bombText.text = "bomb:" + maxBombCount.ToString();
            speedText.text = "speed:" + speed.ToString();

            if (playerID == 1)
            {
                transform.position = MyNetworkManagerObj.GetComponent<MyNetworkManager>().spawn_1.position;
            }
            else
            {
                transform.position = MyNetworkManagerObj.GetComponent<MyNetworkManager>().spawn_2.position;
            }
        }
    }

    [ClientRpc]
    public void ChangeReadyText(int num, bool toTrue)
    {
        ready = toTrue;
        if (num == 1)//server_p1
        {
            if(toTrue == true)
            {
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.text = "p1 ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.color = myGreen;
            }
            else
            {
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.text = "p1 not ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.color = myRed;
            }
        }
        else//client_p2
        {
            if (toTrue == true)
            {
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.text = "p2 ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.color = myGreen;
            }
            else
            {
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.text = "p2 not ready";
                MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.color = myRed;
            }
        }
    }


    [Command]
    public void PlayerJoinToServer()
    {
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.text = "p2 not ready";
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.color = myRed;
        ActiveReadyButton();
    }

    [ClientRpc]
    public void ActiveReadyButton()
    {
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if(!isLocalPlayer)
        {
            return;
        }

        
        if (canMove == true && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            Vector3 myDir = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                myDir += new Vector3(0, 0, 1);
                lastMove = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                myDir += new Vector3(-1, 0, 0);
                lastMove = 2;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                myDir += new Vector3(0, 0, -1);
                lastMove = 3;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                myDir += new Vector3(1, 0, 0);
                lastMove = 4;
            }
            myDir.Normalize();
            rb.velocity = myDir * speed * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if(lastMove == 1)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (lastMove == 2)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (lastMove == 3)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (lastMove == 4)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
            }

        }

        if(Input.GetKeyDown(KeyCode.Space) && canMove == true)
        {
            if(bombCount > 0)
            {
                int myX = 0;
                int myY = 0;
                for (int i = 0; i < 17; i++)
                {
                    if ((-25.5f + (3.0f * i)) <= transform.position.x && transform.position.x <= (-25.5f + (3.0f * i) + 3.0f))
                    {
                        myX = i;
                        break;
                    }
                }
                for (int i = 0; i < 11; i++)
                {
                    if ((16.0f - (3.0f * i)) >= transform.position.z && transform.position.z >= (16.0f - (3.0f * i) - 3.0f))
                    {
                        myY = i;
                        break;
                    }
                }
                if (gridManager.myGrid[myY , myX] == 0)//if the block is empty
                {
                    SpawnBomb(myY, myX, power, playerID);
                    bombCount--;
                }
            }
           
            
        }

        if(!isServer)
        {
            return;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(isLocalPlayer)
        {
            if (other.gameObject.tag == "power")
            {
                if (power < 10)
                {
                    power++;
                    powerText.text = "power:" + power.ToString();
                }
                DestoryPowerUp(other.gameObject);
            }
            if (other.gameObject.tag == "quant")
            {
                if (maxBombCount < 10)
                {
                    bombCount++;
                    maxBombCount++;
                    bombText.text = "bomb:" + maxBombCount.ToString();
                }
                DestoryPowerUp(other.gameObject);
            }
            if (other.gameObject.tag == "speed")
            {
                if (speed < 2)
                {
                    speed += 0.1f;
                    speedText.text = "speed:" + speed.ToString();
                }
                DestoryPowerUp(other.gameObject);
            }
        }
    }

    [ClientRpc]
    public void ChangeColor()
    {
        if(playerID == 1)
        {
            gameObject.GetComponent<MeshRenderer>().material = redSkin;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = yellowSkin;
        }
    }

    [Command]
    public void SpawnBomb(int myY, int myX, int power, int myID)//this will run on server
    {
        ChangeGrid(myY, myX);
        GameObject myBomb = Instantiate(bomb, new Vector3((-25.5f + (3.0f * myX)) + 1.5f, 1.5f, (16.0f - (3.0f * myY) - 1.5f)), new Quaternion(0, 0, 0, 0));
        myBomb.GetComponent<BombBehaviour>().myX = myX;
        myBomb.GetComponent<BombBehaviour>().myY = myY;
        myBomb.GetComponent<BombBehaviour>().power = power;
        myBomb.GetComponent<BombBehaviour>().playerID = myID;
        NetworkServer.Spawn(myBomb);
        gridManager.objList[myY,myX] = myBomb;
    }

    [ClientRpc]
    public void ChangeGrid(int myY, int myX)
    {
        gridManager.myGrid[myY, +myX] = 3;
    }


    [Command]
    public void DestoryPowerUp(GameObject myPowerUp)
    {
        NetworkServer.Destroy(myPowerUp);
    }

    public void GetExploded()
    {
        Died();
    }

    [Command]
    public void Died()
    {
        EndGame();
    }

    [ClientRpc]
    public void EndGame()
    {
        gridManager.GetComponent<GridGenerator>().ClearScene();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        myGoggle.SetActive(false);
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.text = "p1 not ready";
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().p1ReadyText.color = myRed;
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.text = "p2 not ready";
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().p2ReadyText.color = myRed;
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyButtonText.text = "ready";
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyMenu.SetActive(true);
        MyNetworkManagerObj.GetComponent<MyNetworkManager>().readyButton.SetActive(true);
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject myPlayer in playerList)
        {
            myPlayer.GetComponent<PlayerMovement>().ready = false;
            myPlayer.GetComponent<PlayerMovement>().canMove = false;
        }
        if(playerID == 1)//yellow win
        {
            MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.gameObject.SetActive(true);
            MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.text = "yellow win!";
            MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.color = new Color32(243, 255, 136, 255);
        }
        else
        {
            MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.gameObject.SetActive(true);
            MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.text = "red win!";
            MyNetworkManagerObj.GetComponent<MyNetworkManager>().win.color = new Color32(255, 136, 148, 255);
        }
    }
}
