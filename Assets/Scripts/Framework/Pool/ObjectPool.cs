using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool> {

    public string ResourcesDir = "";
    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    public GameObject Spawn(string name,Transform trans)
    {
        SubPool pool = null;
        if (!m_pools.ContainsKey(name))
        {
            RegisterPool(name, trans);
        }
        pool = m_pools[name];

        return pool.Spawn();
    }
    //新建池子
    private void RegisterPool(string name, Transform trans)
    {
        GameObject go = Resources.Load<GameObject>(ResourcesDir + "/" + name);
        SubPool pool = new SubPool(go, trans);
        m_pools.Add(pool.Name, pool);
    }

    public void UnSpawn(GameObject go)
    {
        SubPool pool = null;
        foreach(var poolTemp in m_pools.Values)
        {
            if (poolTemp.Contain(go))
            {
                pool = poolTemp;
                break;
            }
        }
        pool.UnSpawn(go);
    }

    public void UnSpawnAll()
    {
        foreach(var poolTemp in m_pools.Values)
        {
            poolTemp.UnSpawnAll();
        }
    }

    //清空
    public void Clear()
    {
        m_pools.Clear();
    }
}
