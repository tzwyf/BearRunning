using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnManager : EditorWindow
{
    [MenuItem("Tools/ClickMe")]

    static void PatternSystem()
    {
        GameObject spawnManager = GameObject.Find("PatternManager");
        if(spawnManager != null)
        {
            var patternManager = spawnManager.GetComponent<PatternManager>();
            if(Selection.gameObjects.Length == 1)
            {
                var item = Selection.gameObjects[0].transform.Find("Item");
                if(item != null)
                {
                    Pattern pattern = new Pattern();
                    foreach(var child in item)
                    {
                        Transform childTrans = child as Transform;
                        if(childTrans != null)
                        {
                            //找到它对应的游戏物体的预制体
                            var prefab = PrefabUtility.GetPrefabParent(childTrans.gameObject);
                            if(prefab != null)
                            {
                                PatternItem patternItem = new PatternItem()
                                {
                                    pos = childTrans.localPosition,
                                    prefabName = prefab.name

                                };

                                pattern.patternItems.Add(patternItem);
                            }
                        }
                    }
                    patternManager.patterns.Add(pattern);
                }
            }
        }
    }
}
