using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model
{
    //名字标识
    public abstract string Name { get; }

    //发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        //直接调用MVC中定义好的公共方法即可
        MVC.SendEvent(eventName, data);
    }
}