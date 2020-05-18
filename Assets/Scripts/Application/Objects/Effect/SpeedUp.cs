using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Effect {

    public override void OnSpawn()
    {
        transform.localPosition = new Vector3(0, 0, -0.27f);
        transform.localScale = Vector3.one * 3.33f;
        base.OnSpawn();
    }
}
