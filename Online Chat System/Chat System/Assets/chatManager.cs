using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class chatManager : NetworkBehaviour
{
    [SyncVar] public string myName;

    public TMP_InputField inputField;

    public GameObject textObj;

    public GameObject contentBox;

    public GameObject nameField;

    public GameObject inputName;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>();
        contentBox = GameObject.Find("content");
        if(isLocalPlayer)
        {
            inputName = GameObject.Find("inputName");
            nameField = GameObject.Find("nameField");
            myName = nameField.GetComponent<TMP_InputField>().text;
            inputName.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField.text != "")
            {
                if (isServer)
                {
                    //call clientRPC
                    SendMessageToChat("[" + myName + "]" + ":" + inputField.text);
                }
                else
                {
                    //call command
                    SeverDo("[" + myName + "]" + ":" + inputField.text);
                }
                inputField.text = "";
            }
        }
    }

    [ClientRpc]
    public void SendMessageToChat(string message)
    {
        GameObject childOb = Instantiate(textObj, contentBox.transform.position, new Quaternion(0, 0, 0, 0));
        childOb.transform.SetParent(contentBox.transform);
        childOb.GetComponent<TextMeshProUGUI>().text = message;
    }

    [Command]
    public void SeverDo(string message)
    {
        SendMessageToChat(message);
    }

}


