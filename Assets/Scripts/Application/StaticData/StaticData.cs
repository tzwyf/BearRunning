using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoSingleton<StaticData> {
    //足球
    private Dictionary<int, FootballInfo> m_footballData = new Dictionary<int, FootballInfo>();
    public List<Material> footballMatList = new List<Material>();

    //衣服:每一个皮肤会对应着一套衣服
    private Dictionary<int, Dictionary<int, FootballInfo>> m_playerClothesData = new Dictionary<int, Dictionary<int, FootballInfo>>();
    public List<Material> playerSkinMatList = new List<Material>();

    private void InitFootball()
    {
        m_footballData.Add(0, new FootballInfo() { footballMat = footballMatList[0], coin = 0 });
        m_footballData.Add(1, new FootballInfo() { footballMat = footballMatList[1], coin = 500 });
        m_footballData.Add(2, new FootballInfo() { footballMat = footballMatList[2], coin = 1000 });
    }

    private void InitPlayerClothes()
    {
        int t = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (!m_playerClothesData.ContainsKey(i))
                {
                    m_playerClothesData.Add(i,new Dictionary<int, FootballInfo>());
                }
                //print(t * 200);
                m_playerClothesData[i].Add(j, new FootballInfo() { coin = t * 200, footballMat = playerSkinMatList[t] });
                t++;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //初始化足球
        InitFootball();
        //初始化衣服
        InitPlayerClothes();
    }

    public FootballInfo GetFootballInfo(int i)
    {
        return m_footballData[i];
    }
    public FootballInfo GetPlayerClothesInfo(int i,int j)
    {
        return m_playerClothesData[i][j];
    }
}
