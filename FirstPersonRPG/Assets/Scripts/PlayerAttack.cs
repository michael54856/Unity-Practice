using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int weaponType;
    public Animator handAnimator;

    public float swordAttackSpeed;

    public bool isPlayingSwing01;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponType == 1)
        {
            if(handAnimator.GetCurrentAnimatorStateInfo(0).IsName("sword_Idle") || handAnimator.GetCurrentAnimatorStateInfo(0).IsName("sword_running"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    handAnimator.SetTrigger("swing01");
                }
            }
            if(isPlayingSwing01)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    handAnimator.SetBool("swing02", true);
                }
            }
        }
    }
}
