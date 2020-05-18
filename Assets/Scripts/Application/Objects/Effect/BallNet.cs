using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNet : Effect {
    public override void OnSpawn()
    {

        transform.localPosition = new Vector3(0, 0, -1.6f);
        transform.localScale = Vector3.one * 1.6f;
        base.OnSpawn();
    }

    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}
