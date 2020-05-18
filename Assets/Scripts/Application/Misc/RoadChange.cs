using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChange : MonoBehaviour {

    private GameObject roadNow;
    private GameObject roadNext;
    private GameObject parent;

	void Start () {
		if(parent == null)
        {
            parent = new GameObject("Road");
            parent.transform.position = Vector3.zero;
        }

        //先初始化生成两条路
        roadNow = Game.Instance.objectPool.Spawn("Pattern_1", parent.transform);
        roadNow.transform.SetParent(parent.transform);
        roadNext = Game.Instance.objectPool.Spawn("Pattern_2", parent.transform);
        roadNext.transform.SetParent(parent.transform);
        roadNext.transform.position += new Vector3(0, 0, 160);

        //在初始的跑道上添加Item
        AddItem(roadNow);
        AddItem(roadNext);
    }
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.road)
        {
            //回收旧的跑道
            Game.Instance.objectPool.UnSpawn(other.gameObject);

            //创建新的跑道
            SpawnNewRoad();
        }
    }
    //创建新的跑道
    private void SpawnNewRoad()
    {
        int index = Random.Range(1, 5);

        roadNow = roadNext;
        roadNext = Game.Instance.objectPool.Spawn("Pattern_" + index, parent.transform);
        roadNext.transform.position = roadNow.transform.position + new Vector3(0, 0, 160);

        //在新跑道上添加Item
        AddItem(roadNext);
    }

    public void AddItem(GameObject obj)
    {
        var childItemTrans = obj.transform.Find("Item");
        var partternManager = PatternManager.Instance;
        if (partternManager != null && partternManager.patterns != null && partternManager.patterns.Count > 0)
        {
            //随机取一套方案
            var pattern = partternManager.patterns[Random.Range(0, partternManager.patterns.Count)];
            if(pattern != null && pattern.patternItems != null && pattern.patternItems.Count > 0)
            {
                foreach(var item in pattern.patternItems)
                {
                    if(item != null)
                    {
                        GameObject go = Game.Instance.objectPool.Spawn(item.prefabName, childItemTrans);
                        //再次强制设置下父物体
                        go.transform.SetParent(childItemTrans);
                        go.transform.localPosition = item.pos;
                    }
                }
            }
        }
    }
}
