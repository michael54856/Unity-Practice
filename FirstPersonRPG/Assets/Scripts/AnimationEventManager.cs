using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public TrailRenderer swordTrail;

    public PlayerAttack player;

    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Swing01_Start()
    {
        swordTrail.emitting = true;
        player.isPlayingSwing01 = true;
    }
    public void Swing01_End()
    {
        swordTrail.emitting = false;
        player.isPlayingSwing01 = false;
    }

    public void Swing02_Start()
    {
        swordTrail.emitting = true;
    }
    public void Swing02_End()
    {
        swordTrail.emitting = false;
        handAnimator.SetBool("swing02", false);
    }
}
