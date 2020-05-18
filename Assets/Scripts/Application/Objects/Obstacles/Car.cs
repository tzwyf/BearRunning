using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Obstacles {

    public bool canMove = false;
    private bool isBlock = false;
    public float speed = 10;
    private GameModel gm;
    protected override void Awake()
    {
        base.Awake();
        gm = MVC.GetModel<GameModel>();
    }
    public override void HitPlayer(Vector3 pos)
    {
        base.HitPlayer(pos);
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void OnUnSpawn()
    {
        isBlock = false;
        base.OnUnSpawn();
    }

    //碰撞到触发区域
    public void HitTrigger()
    {
        isBlock = true;
    }

    private void Update()
    {
        if(isBlock && canMove && !gm.IsPause && gm.IsPlay)
            transform.Translate(-transform.forward * speed * Time.deltaTime);
    }
}
