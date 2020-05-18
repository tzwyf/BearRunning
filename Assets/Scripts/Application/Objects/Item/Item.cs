using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ReusableObject {

    public float speed = 60;

    public override void OnSpawn()
    {
        
    }

    public override void OnUnSpawn()
    {
        transform.localEulerAngles = Vector3.zero;
    }

	void Update () {
        transform.Rotate(0, speed * Time.deltaTime, 0);
	}
    public virtual void HitPlayer(Transform trans)
    {

    }
}
