using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManageButton : MonoBehaviour
{
    public GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gm.SetActive(true);
        Destroy(gameObject);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
