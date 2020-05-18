using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatternManager : MonoSingleton<PatternManager> {

    public List<Pattern> patterns = new List<Pattern>();
}

[Serializable]
//一个游戏物体的信息
public class PatternItem
{
    public string prefabName;
    public Vector3 pos;
}

[Serializable]
//一套方案
public class Pattern
{
    public List<PatternItem> patternItems = new List<PatternItem>();
}