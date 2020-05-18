using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : MonoSingleton<Game> {

    //全局访问
    [HideInInspector]
    public ObjectPool objectPool;
    [HideInInspector]
    public Sound sound;
    [HideInInspector]
    public StaticData staticData;


	void Start () {
        //加载其他场景时不会被销毁
        DontDestroyOnLoad(gameObject);

        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;
        

        //初始化注册StartUpController
        RegisterController(Consts.E_StartUpEventName, typeof(StartUpController));

        //游戏启动
        SendEvent(Consts.E_StartUpEventName);

        //跳转场景
        SceneManager.LoadScene(1);
    }

    //自定义加载场景的方法
    public void LoadLevel(int level)
    {
        //发送退出场景事件
        ScenesArgs e = new ScenesArgs
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex
        };
        SendEvent(Consts.E_ExitSceneEventName, e);

        //加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    //当场景被加载的时候自动调用 level为加载的新场景的索引
    private void OnLevelWasLoaded(int level)
    {
        //Debug.Log("进入新场景" + level);
        //发送进入新场景事件
        ScenesArgs e = new ScenesArgs
        {
            sceneIndex = level
        };
        SendEvent(Consts.E_EnterSceneEventName, e);
    }

    //发送事件
    void SendEvent(string eventName, object data = null)
    {
        //直接调用MVC中定义好的公共方法即可
        MVC.SendEvent(eventName, data);
    }

    //用于注册StartUpController
    void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }
}
