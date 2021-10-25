using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkManager : NetworkManager
{
    public Transform spawn_1;
    public Transform spawn_2;

    public GridGenerator gridGen;

    public GameObject readyButton;
    public GameObject powerList;
    public GameObject readyMenu;
    public TextMeshProUGUI p1ReadyText;
    public TextMeshProUGUI p2ReadyText;
    public TextMeshProUGUI readyButtonText;

    private Color32 myGreen = new Color32(165, 255, 180, 255);
    private Color32 myBlue = new Color32(165, 248, 255, 255);
    private Color32 myRed = new Color32(255, 150, 150, 255);

    public GameObject gridManager;
    public TextMeshProUGUI win;

    public struct CreateCharacterMessage : NetworkMessage
    {
        public int playerID;
    }
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);//this will deal with the message,and send to OnCreateCharacter 

       
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        if(numPlayers == 1)//when p2 disconnect,server still exist
        {
            readyButton.SetActive(false);
            powerList.SetActive(true);
            readyMenu.SetActive(true);
            win.gameObject.SetActive(false);
            p1ReadyText.text = "p1 not ready";
            p1ReadyText.color = myRed;
            p2ReadyText.text = "wait for join...";
            p2ReadyText.color = myBlue;
            readyButtonText.text = "ready";
            GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject myPlayer in playerList)//set the server player's ready to false
            {
                if (myPlayer.GetComponent<PlayerMovement>().isLocalPlayer)//find local player
                {
                    myPlayer.GetComponent<PlayerMovement>().ready = false;
                    myPlayer.GetComponent<PlayerMovement>().canMove = false;
                    myPlayer.transform.position = spawn_1.position;
                    //clearScene
                    gridManager.GetComponent<GridGenerator>().ClearScene();
                }
            }
        }
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        if(numPlayers >= 2)
        {
            conn.Disconnect();
            return;
        }
    }
    public override void OnClientConnect(NetworkConnection conn)//run on client
    {
        base.OnClientConnect(conn);
        // you can send the message here, or wherever else you want
        CreateCharacterMessage characterMessage = new CreateCharacterMessage
        {
            playerID = 0
        };
        powerList.SetActive(true);
        readyMenu.SetActive(true);
        conn.Send(characterMessage);
        
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        powerList.SetActive(false);
        readyMenu.SetActive(false);
        readyButton.SetActive(false);
        win.gameObject.SetActive(false);
        gridManager.GetComponent<GridGenerator>().ClearScene();
    }

    void OnCreateCharacter(NetworkConnection conn, CreateCharacterMessage message)//this will run on server
    {
        if(numPlayers == 0)
        {
            GameObject playerObj = Instantiate(playerPrefab, spawn_1.transform.position, new Quaternion(0, 0, 0, 0));
            playerObj.GetComponent<PlayerMovement>().playerID = 1;
            NetworkServer.AddPlayerForConnection(conn, playerObj);
            playerObj.GetComponent<PlayerMovement>().ChangeColor();
        }
        else
        {
            GameObject playerObj = Instantiate(playerPrefab, spawn_2.transform.position, new Quaternion(0, 0, 0, 0));
            playerObj.GetComponent<PlayerMovement>().playerID = 2;
            NetworkServer.AddPlayerForConnection(conn, playerObj);
            playerObj.GetComponent<PlayerMovement>().ChangeColor();
        }
       
    }
}
