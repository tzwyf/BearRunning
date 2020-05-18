using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ReusableObject
{
    public float waitTime = 1;

    public override void OnSpawn()
    {
        StartCoroutine(DestoryCoroutine());
    }

    public override void OnUnSpawn()
    {
        StopAllCoroutines();
    }

    IEnumerator DestoryCoroutine()
    {
        yield return new WaitForSeconds(waitTime);

        Game.Instance.objectPool.UnSpawn(gameObject);
    }


}
