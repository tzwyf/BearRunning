using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffect : Effect {

    public override void OnSpawn()
    {

        base.OnSpawn();
        transform.localPosition = new Vector3(0, 5.5f, 7.5f);
        transform.localScale = new Vector3(3.3f, 3.3f, 3.3f);
    }
    public override void OnUnSpawn()
    {
        base.OnUnSpawn();
    }
}
