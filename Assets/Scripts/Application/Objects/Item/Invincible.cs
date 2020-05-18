using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : Item {

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

        //2、声音
        Game.Instance.sound.PlayEffect("Se_UI_Whist");

        //3、回收
        Game.Instance.objectPool.UnSpawn(gameObject);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.player)
        {
            HitPlayer(other.transform);
            //other.SendMessage("HitWhist", SendMessageOptions.RequireReceiver);
            other.SendMessage("HitItem", ItemKind.InvincibleItem);
        }
    }
}
