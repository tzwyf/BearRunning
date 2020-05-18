using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
    #region 常量
    private const float startTime = 50;
    #endregion

    #region 事件
    #endregion

    #region 字段
    private int m_coin = 0;
    private float m_distance = 0;
    private float m_goalCount = 0;
    private float m_skillTime;
    private float m_timer;
    private GameModel gm;

    public Text coinText;
    public Text distanceText;
    public Text timeText;
    public Text gizmoMagnetText;//技能持续时间
    public Text gizmoInvincibleText;
    public Text gizmoMultiplyText;

    public Slider timerSlider;
    public Button invincibleButton;//技能点击按钮
    public Button multiplyButton;
    public Button magnetButton;
    public Slider goalSlider;
    public Button goalButton;
    
    private IEnumerator MultiplyCor;//双倍金币协程
    private IEnumerator MagnetCor;
    private IEnumerator InvincibleCor;

    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_BoardName;
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
            coinText.text = value.ToString();
        }
    }

    public float Distance
    {
        get
        {
            return m_distance;
        }

        set
        {
            m_distance = value;
            distanceText.text = value.ToString() + "米";
        }
    }

    public float Timer
    {
        get
        {
            return m_timer;
        }

        set
        {
            if(value < 0)
            {
                value = 0;
                SendEvent(Consts.E_EndGameEventName);
            }
            else if(value > startTime)
            {
                value = startTime;
            }
            m_timer = value;
            timeText.text = m_timer.ToString("f2");
            timerSlider.value = m_timer / startTime;

        }
    }

    public float GoalCount
    {
        get
        {
            return m_goalCount;
        }

        set
        {
            m_goalCount = value;
        }
    }
    #endregion

    #region 方法
    //更新UI 技能按钮是否可以点击
    public void UpdateUI()
    {
        ShowOrHide(gm.Invincible, invincibleButton);
        ShowOrHide(gm.Multiply, multiplyButton);
        ShowOrHide(gm.Magnet, magnetButton);
    }
    private void ShowOrHide(int count, Button btn)
    {
        if (count > 0)
        {
            btn.interactable = true;
            btn.transform.Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            btn.interactable = false;
            btn.transform.Find("Mask").gameObject.SetActive(true);
        }
    }

    //暂停按钮点击
    public void OnPauseButtonClick()
    {
        PauseArgs e = new PauseArgs()
        {
            coin = Coin,
            distance = Distance,
            score = Coin + Distance * (GoalCount + 1)

        };
        SendEvent(Consts.E_PauseGameEventName,e);
    }

    //UI更新
    //双倍金币
    public void HitMultiply()
    {
        //控制单一协程
        if (MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }
    IEnumerator MultiplyCoroutine()
    {
        float timer = m_skillTime;
        gizmoMultiplyText.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                timer -= Time.deltaTime;
                gizmoMultiplyText.text = GetTime(timer);
            }
            yield return 0;
        }
        gizmoMultiplyText.transform.parent.gameObject.SetActive(false);
    }


    //吸铁石
    public void HitMagnet()
    {
        //控制单一协程
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }
    IEnumerator MagnetCoroutine()
    {
        float timer = m_skillTime;
        gizmoMagnetText.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                timer -= Time.deltaTime;
                gizmoMagnetText.text = GetTime(timer);
            }
            yield return 0;
        }
        gizmoMagnetText.transform.parent.gameObject.SetActive(false);
    }

    //吃到哨子进入无敌状态，无视大小Fence
    public void HitWhist()
    {
        //控制单一协程
        if (InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvinclbleCoroutine();
        StartCoroutine(InvincibleCor);
    }
    IEnumerator InvinclbleCoroutine()
    {
        float timer = m_skillTime;
        gizmoInvincibleText.transform.parent.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                timer -= Time.deltaTime;
                gizmoInvincibleText.text = GetTime(timer);
            }
            yield return 0;
        }
        gizmoInvincibleText.transform.parent.gameObject.SetActive(false);
    }

    private string GetTime(float time)
    {
        return ((int)time + 1).ToString();
    }


    //技能按钮点击
    public void OnInvincibleButtonClick()
    {
        ItemArgs e = new ItemArgs()
        {
            hitCount = 1,
            kind = ItemKind.InvincibleItem

        };
        SendEvent(Consts.E_HitItemEventName, e);
    }
    public void OnMultiplyButtonClick()
    {
        ItemArgs e = new ItemArgs()
        {
            hitCount = 1,
            kind = ItemKind.MultiplyItem

        };
        SendEvent(Consts.E_HitItemEventName, e);
    }
    public void OnMagnetButtonClick()
    {
        ItemArgs e = new ItemArgs()
        {
            hitCount = 1,
            kind = ItemKind.MagnetItem

        };
        SendEvent(Consts.E_HitItemEventName, e);
    }

    //射门滑动条显示，按钮可以按下
    private void ShowGoalClick()
    {
        //1、使用协程控制滑动条显示 按钮可以按下
        StartCoroutine(StartCountDown());
    }
    IEnumerator StartCountDown()
    {
        goalButton.interactable = true;
        goalSlider.value = 1;
        while(goalSlider.value > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                goalSlider.value -= 1.25f * Time.deltaTime;
            }
            yield return 0;
        }
        goalButton.interactable = false;
        goalSlider.value = 0;
    }

    //射门按钮点击
    public void OnGoalButtonClick()
    {
        //向PlayerMove发送一个事件
        SendEvent(Consts.E_ClickGoalButtonEventName);
        goalSlider.value = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    #endregion

    #region Unity回调
    private void Awake()
    {
        gm = GetModel<GameModel>();
        Timer = startTime;
        UpdateUI();
        m_skillTime = gm.SkillTime;
    }

    private void Update()
    {
        if (!gm.IsPause && gm.IsPlay)
            Timer -= Time.deltaTime;
    }
    #endregion

    #region 事件回调
    public override void RegisterAttentionEvent()
    {
        AttentionEventList.Add(Consts.E_UpdateDisEventName);
        AttentionEventList.Add(Consts.E_UpdateCoinEventName);
        AttentionEventList.Add(Consts.E_AddTimeEventName);
        AttentionEventList.Add(Consts.E_HitGoalTriggerEventName);
        AttentionEventList.Add(Consts.E_ShootGoalEventName);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_UpdateDisEventName:
                DistanceArgs e = data as DistanceArgs;
                Distance = e.distance;
                break;
            case Consts.E_UpdateCoinEventName:
                CoinArgs e2 = data as CoinArgs;
                Coin += e2.coin;
                break;
            case Consts.E_AddTimeEventName:
                Timer += 20;
                break;
            case Consts.E_HitGoalTriggerEventName:
                ShowGoalClick();
                break;
            case Consts.E_ShootGoalEventName:
                GoalCount += 1;
                //print("进了" + GoalCount + "个球");
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
