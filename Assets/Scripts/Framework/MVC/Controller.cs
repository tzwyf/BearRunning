using UnityEngine;
using System.Collections;
using System;

public abstract class Controller
{
    //执行事件
    public abstract void Execute(object data);

    //获取模型
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    //获取视图
    protected T GetView<T>()
        where T : View
    {
        return MVC.GetView<T>() as T;
    }

    //注册模型、视图、控制器
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
}
