using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoal : ReusableObject {
    public Animation goalKeeper;
    public Animation goalDoor;
    public GameObject ballNet;
    public float speed = 20;

    private bool m_isFly = false;
    private GameModel gm;

    public override void OnSpawn()
    {
        goalKeeper.Play("standard");
    }

    public override void OnUnSpawn()
    {
        goalKeeper.transform.parent.parent.gameObject.SetActive(true);
        goalKeeper.Play("standard");
        goalDoor.Play("QiuMen_St");
        ballNet.SetActive(true);
        m_isFly = false;
    }

    //进球了,孩子会发送一个消息来执行这个方法
    public void ShootGoalIn(int i)
    {
        switch (i)
        {
            case -2:
                goalKeeper.Play("left_flutter");
                break;
            case 0:
                goalKeeper.Play("flutter");
                break;
            case 2:
                goalKeeper.Play("right_flutter");
                break;
        }
        StartCoroutine(HideGoalKeeper());
    }
    IEnumerator HideGoalKeeper()
    {
        yield return new WaitForSeconds(0.7f);
        goalKeeper.transform.parent.parent.gameObject.SetActive(false);
    }

    //守门员被撞飞
    public void HitGoalKeeper()
    {
        m_isFly = true;
        goalKeeper.Play("fly");
    }
    //守门员被撞飞2秒后将m_isFly设为false
    IEnumerator FlyTime()
    {
        yield return new WaitForSeconds(2f);
        m_isFly = false;
    }

    public void HitDoor(int i)
    {
        //播放球门动画
        switch (i)
        {
            case 0:
                goalDoor.Play("QiuMen_RR");
                break;
            case 1:
                goalDoor.Play("QiuMen_St");
                break;
            case 2:
                goalDoor.Play("QiuMen_LR");
                break;
        }
        //球网消失
        ballNet.SetActive(false);
    }

    private void Awake()
    {
        gm = MVC.GetModel<GameModel>();
    }

    void Update () {
        if (m_isFly)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                goalKeeper.transform.localPosition += new Vector3(0, speed, -speed) * Time.deltaTime;
            }
            
        }
        else
        {
            goalKeeper.transform.localPosition = new Vector3(0, 0, -0.49f);
        }
	}
}
