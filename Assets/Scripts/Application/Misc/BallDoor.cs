using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDoor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.ball)
        {
            other.transform.parent.parent.SendMessage("HitBallDoor", SendMessageOptions.RequireReceiver);
            transform.parent.parent.SendMessage("ShootGoalIn", (int)other.transform.position.x);
        }
    }
}
