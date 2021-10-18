using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class MyNetworkManager : NetworkManager
{
    public GameObject chatSystem;

    public GameObject inputName;
    public GameObject nameField;
    public GameObject content;


    public override void OnClientConnect(NetworkConnection conn)//call when client connect to server
    {
        base.OnClientConnect(conn);
        chatSystem.SetActive(true);
        foreach (Transform child in content.transform)//clear message
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn)//call when client disconnect to server
    {
        base.OnClientDisconnect(conn);
        chatSystem.SetActive(false);

        inputName.SetActive(true);
        nameField.GetComponent<TMP_InputField>().text = "";
    }
}
