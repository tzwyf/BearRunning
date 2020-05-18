using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class MVC
{
    //资源
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();//名字-Model
    public static Dictionary<string, View> Views = new Dictionary<string, View>();//名字-View
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();//事件名字-类型

    //注册模型、视图、控制器
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }
    public static void RegisterView(View view)
    {
        //防止view的重复注册
        if (Views.ContainsKey(view.Name))
            Views.Remove(view.Name);

        view.RegisterAttentionEvent();
        Views[view.Name] = view;
    }
    public static void RegisterController(string eventName,Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    //获取Model
    public static T GetModel<T>() where T:Model {
        foreach(var m in Models.Values)
        {
            if (m is T)
                return (T)m;
        }
        return null;
    }
    //获取View
    public static T GetView<T>() where T : View
    {
        foreach (var v in Views.Values)
        {
            if (v is T)
                return (T)v;
        }
        return null;
    }


    //发送事件
    public static void SendEvent(string eventName,object data = null)
    {
        //controller执行
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            //控制器生成
            Controller c = Activator.CreateInstance(t) as Controller;
            c.Execute(data);
        }

        //View处理
        foreach(var v in Views.Values)
        {
            if (v.AttentionEventList.Contains(eventName))
            {
                v.HandleEvent(eventName,data);
            }
        }
    }
}
