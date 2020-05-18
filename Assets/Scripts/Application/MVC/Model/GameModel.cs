using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{

    #region 常量
    private int InitCoin = 5000;
    #endregion

    #region 事件
    #endregion

    #region 字段
    private bool m_isPlay = true;
    private bool m_isPause = false;

    private float m_skillTime;
    private int m_magnet;
    private int m_invincible;
    private int m_multiply;
    private int m_grade;
    private float m_exp;
    private int m_coin;
    //足球
    private int m_footballEquipped = 0;
    public List<int> m_footballBought = new List<int>();

    //衣服
    private BuyID m_playerClothesEquipped = new BuyID() { skinId = 0, clothesId = 0 };
    public List<BuyID> m_playerClothesBought = new List<BuyID>();

    public int lastIndex = 1;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.M_GameModel;
        }
    }

    public bool IsPlay
    {
        get
        {
            return m_isPlay;
        }

        set
        {
            m_isPlay = value;
        }
    }

    public bool IsPause
    {
        get
        {
            return m_isPause;
        }

        set
        {
            m_isPause = value;
        }
    }

    public float SkillTime
    {
        get
        {
            return m_skillTime;
        }

        set
        {
            m_skillTime = value;
        }
    }

    public int Magnet
    {
        get
        {
            return m_magnet;
        }

        set
        {
            m_magnet = value;
        }
    }

    public int Invincible
    {
        get
        {
            return m_invincible;
        }

        set
        {
            m_invincible = value;
        }
    }

    public int Multiply
    {
        get
        {
            return m_multiply;
        }

        set
        {
            m_multiply = value;
        }
    }

    public int Grade
    {
        get
        {
            return m_grade;
        }

        set
        {
            m_grade = value;
        }
    }

    public float Exp
    {
        get
        {
            return m_exp;
        }

        set
        {
            while(value > 500 + Grade * 100)
            {
                value -= (500 + Grade * 100);
                Grade++;
            }
            m_exp = value;
        }
    }

    public int Coin
    {
        get
        {
            return m_coin;
        }

        set
        {
            m_coin = value;
            //Debug.Log("还剩" + value + "钱");
        }
    }

    public int FootballEquipped
    {
        get
        {
            return m_footballEquipped;
        }

        set
        {
            m_footballEquipped = value;
        }
    }

    public BuyID PlayerClothesEquipped
    {
        get
        {
            return m_playerClothesEquipped;
        }

        set
        {
            m_playerClothesEquipped = value;
        }
    }
    #endregion

    #region 方法

    public bool IsMoneyEnough(int coin)
    {
        if(coin <= Coin)
        {
            Coin -= coin;
            return true;
        }
        return false;
    }

    public void Init()
    {
        m_skillTime = 5;
        m_invincible = 0;
        m_multiply = 0;
        m_magnet = 0;
        m_grade = 1;
        m_exp = 0;
        Coin = InitCoin;

        InitSkin();
    }

    private void InitSkin()
    {
        //足球
        m_footballBought.Add(FootballEquipped);
        //衣服
        m_playerClothesBought.Add(PlayerClothesEquipped);
    }

    //根据索引获取足球的购买或装备状态
    public ItemState GetFootBallState(int i)
    {
        if(i == FootballEquipped)
        {
            return ItemState.Equip;
        }
        else
        {
            if (m_footballBought.Contains(i))
            {
                return ItemState.Buy;
            }
            else
            {
                return ItemState.UnBuy;
            }
        }
    }

    //根据索引获取玩家皮肤的购买或装备状态
    public ItemState GetPlayerSkinState(int i)
    {
        if (i == PlayerClothesEquipped.skinId)
        {
            return ItemState.Equip;
        }
        else
        {
            foreach(var v in m_playerClothesBought)
            {
                if (i == v.skinId)
                {
                    return ItemState.Buy;
                }
            }
            return ItemState.UnBuy;
        }
    }

    //根据BuyID获取玩家衣服的购买或装备状态
    public ItemState GetPlayerClothesState(BuyID id)
    {
        if (id.skinId == PlayerClothesEquipped.skinId && id.clothesId == PlayerClothesEquipped.clothesId)
        {
            return ItemState.Equip;
        }
        else
        {
            foreach (var v in m_playerClothesBought)
            {
                if (v.skinId == id.skinId && v.clothesId == id.clothesId)
                {
                    return ItemState.Buy;
                }
            }

            return ItemState.UnBuy;
        }
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion

}

public class BuyID
{
    public int skinId;
    public int clothesId;
}