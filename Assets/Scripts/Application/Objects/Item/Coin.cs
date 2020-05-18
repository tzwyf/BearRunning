using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item {

    private Transform effectParent;
    public float moveSpeed = 40;

    private void Awake()
    {
        effectParent = GameObject.Find("EffectParent").transform;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
    public override void HitPlayer(Transform trans)
    {
        //1、特效
        GameObject go = Game.Instance.objectPool.Spawn("FX_JinBi", effectParent);
        go.transform.position = trans.position;

        //2、声音
        Game.Instance.sound.PlayEffect("Se_UI_JinBi");

        //3、回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.player)
        {
            HitPlayer(other.transform);
            other.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
        }
        else if(other.tag == Tag.magnetCollider)
        {
            //飞向主角
            StartCoroutine(HitMagnetCollider(other.transform));
        }
    }

    IEnumerator HitMagnetCollider(Transform trans)
    {
        bool isLoop = true;
        while (isLoop)
        {
            transform.position = Vector3.Lerp(transform.position, trans.position, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(trans.position,transform.position) < 0.5f)
            {
                isLoop = false;
                HitPlayer(trans);
                trans.parent.SendMessage("HitCoin", SendMessageOptions.RequireReceiver);
            }

            yield return 0;
        }
    }
}
