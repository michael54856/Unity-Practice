using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenESC : MonoBehaviour
{

    public bool stop = false;

    public GameObject resumeButton;
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(stop == false)
            {
                Time.timeScale = 0;
                resumeButton.SetActive(true);
                restartButton.SetActive(true);
                stop = true;
            }
            else
            {
                Time.timeScale = 1;
                resumeButton.SetActive(false);
                restartButton.SetActive(false);
                stop = false;
            }

            
        }
    }
}
