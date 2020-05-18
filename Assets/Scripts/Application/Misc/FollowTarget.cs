using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    private Transform m_PlayerTrans;
    private Vector3 offset;
    public float speed = 20;

    private void Awake()
    {
        m_PlayerTrans = GameObject.FindGameObjectWithTag(Tag.player).transform;
        offset = transform.position - m_PlayerTrans.position;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, m_PlayerTrans.position + offset, speed * Time.deltaTime);
    }
}
