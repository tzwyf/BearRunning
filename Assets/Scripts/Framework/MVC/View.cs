using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour {
    //名字标识
	public abstract string Name { get; }

    //关心事件列表
    [HideInInspector]
    public List<string> AttentionEventList = new List<string>();
    //向关心事件列表添加关心事件：由于每个View的关心事件不同，所以要设置为virtual
    public virtual void RegisterAttentionEvent()
    {

    }

    //获取模型
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    //处理事件
    public abstract void HandleEvent(string eventName, object data);

    //发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        //直接调用MVC中定义好的公共方法即可
        MVC.SendEvent(eventName, data);
    }
}
