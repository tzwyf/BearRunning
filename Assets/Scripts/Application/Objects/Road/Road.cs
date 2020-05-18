using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : ReusableObject
{
    public override void OnSpawn()
    {
        
    }

    public override void OnUnSpawn()
    {
        //回收Item下的东西
        var childItemTrans = transform.Find("Item");
        if(childItemTrans != null)
        {
            foreach(Transform child in childItemTrans)
            {
                if(child != null)
                {
                    Game.Instance.objectPool.UnSpawn(child.gameObject);
                }
            }
        }

    }
}
