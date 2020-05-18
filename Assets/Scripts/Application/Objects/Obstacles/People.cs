using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Obstacles {

    public bool isTrigger = false;
    public bool isFly = false;
    public float speed = 10;

    private Animation anim;
    private GameModel gm;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponentInChildren<Animation>();
        gm = MVC.GetModel<GameModel>();
    }
    public override void HitPlayer(Vector3 pos)
    {
        //播放撞击特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = pos;
        isTrigger = false;
        isFly = true;

        anim.Play("fly");
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        anim.Play("run");
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
        anim.transform.position = Vector3.zero;
        isTrigger = false;
        isFly = false;
    }

    //碰撞到触发区域
    public void HitTrigger()
    {
        isTrigger = true;
        StartCoroutine(GoBack());
    }
    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(2);
        transform.localPosition = new Vector3(2, 0, 82.37f);
    }

    private void Update()
    {
        if (isTrigger && !gm.IsPause && gm.IsPlay)
        {
            transform.localPosition -= new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (isFly && !gm.IsPause && gm.IsPlay)
        {
            transform.localPosition += new Vector3(0, speed, speed) * Time.deltaTime;
        }
    }
}
