using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI hp;

    public GameObject gm;

    public GameObject youDied;
    public GameObject restartButton;

    public GameObject playerSprite;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -7)
        {
            Damage(100000);
        }

    }

    public void FixedUpdate()
    {
        anim.SetBool("walking", false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * Time.fixedDeltaTime * 5);
            anim.SetBool("walking", true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Time.fixedDeltaTime * 5);
            anim.SetBool("walking", true);
        }

    }

    public void Damage(int adj)
    {
        health-=adj;
        hp.text = ":" + health.ToString();

        if (health <= 0)
        {
            hp.text = ":0";
            Time.timeScale = 0;
            gm.SetActive(false);
            youDied.SetActive(true);
            restartButton.SetActive(true);
            playerSprite.SetActive(false);


        }
    }

    public void Heal(int adj)
    {
        health+=adj;
        hp.text = ":" + health.ToString();
    }
}