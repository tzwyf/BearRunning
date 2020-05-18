using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool {

    private List<GameObject> m_objects = new List<GameObject>();
    private GameObject m_prefab;
    public string Name
    {
        get { return m_prefab.name; }
    }
    private Transform m_parent;

    public SubPool(GameObject prefab,Transform parent)
    {
        m_prefab = prefab;
        m_parent = parent;
    }

    public GameObject Spawn()
    {
        GameObject go = null;
        foreach(var goTemp in m_objects)
        {
            if (goTemp != null && !goTemp.activeSelf)
            {
                go = goTemp;
                
                break;
            }
        }

        if (go == null)
        {
            go = GameObject.Instantiate(m_prefab);
            go.transform.SetParent(m_parent);
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

        return go;
    }

    public void UnSpawn(GameObject go)
    {
        if (Contain(go))
        {
            go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    public void UnSpawnAll()
    {
        foreach(var go in m_objects)
        {
            if(go.activeSelf)
                UnSpawn(go);
        }
    }

    //判断go是否在该池的list中
    public bool Contain(GameObject go)
    {
        return m_objects.Contains(go);
    }
}
