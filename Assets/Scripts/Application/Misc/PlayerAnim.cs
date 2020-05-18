using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : View {

    private Animation anim;
    private Action playAnim;
    private GameModel gm;

    public override string Name
    {
        get
        {
            return Consts.V_PlayerAnimName;
        }
    }

    void Awake () {
        anim = GetComponent<Animation>();
        playAnim = PlayRun;
        gm = GetModel<GameModel>();
    }

    void Update()
    {
        if(playAnim != null)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                playAnim();
            }
            else
            {
                anim.Stop();
            }
                
        }
    }

    private void PlayRun()
    {
        anim.Play("run");
    }
    private void PlayLeft()
    {
        anim.Play("left_jump");
        if(anim["left_jump"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    private void PlayRight()
    {
        anim.Play("right_jump");
        if (anim["right_jump"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    private void PlayRoll()
    {
        anim.Play("roll");
        if (anim["roll"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    private void PlayJump()
    {
        anim.Play("jump");
        if (anim["jump"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    private void PlayShoot()
    {
        anim.Play("Shoot01");
        if (anim["Shoot01"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    //执行PlayerMove发送过来的射门消息
    public void MessagePlayShoot()
    {
        playAnim = PlayShoot;
    }

    public void AnimManager(InputDirection dir)
    {
        switch (dir)
        {
            case InputDirection.Null:
                break;
            case InputDirection.Right:
                playAnim = PlayRight;
                break;
            case InputDirection.Left:
                playAnim = PlayLeft;
                break;
            case InputDirection.Up:
                playAnim = PlayJump;
                break;
            case InputDirection.Down:
                playAnim = PlayRoll;
                break;
            default:
                break;
        }


    }

    public override void HandleEvent(string eventName, object data)
    {
        throw new NotImplementedException();
    }
}