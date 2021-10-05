using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyGameManager : MonoBehaviour
{
    public Transform origin;
    public int ammo = 10;

    public bool startCountDown = false;
    public float countDownTime = 2f;

    public GameObject playerBall;
    public TextMeshProUGUI ammoText;

    public bool startSpawn = false;
    public float spawnTime;

    public GameObject restartButton;


    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(startCountDown == true)
        {
            countDownTime -= Time.deltaTime;
            if(countDownTime < 0)
            {
                restartButton.SetActive(true);
            }
        }

        if(startSpawn == true)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime < 0)
            {
                Instantiate(playerBall, origin.transform.position, Quaternion.identity);
                startSpawn = false;
            }
        }
    }

    public void CreatePlayerBall()
    {
        if(ammo == 0)
        {
            startCountDown = true;
            ammoText.text = "ammo:0";
        }
        else
        {
            startSpawn = true;
            spawnTime = 0.5f;
            ammo--;
            ammoText.text = "ammo:" + ammo.ToString();
        }
    }
}
