using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDead : View
{
    //贿赂次数
    private int briberyTime = 1;
    public Text briberyText;

    public override string Name
    {
        get
        {
            return Consts.V_DeadName;
        }
    }

    public int BriberyTime
    {
        get
        {
            return briberyTime;
        }

        set
        {
            briberyTime = value;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }
    //点击关闭按钮
    public void OnCloseButtonClick()
    {
        SendEvent(Consts.E_FinalUIEventName);
    }

    //点击贿赂按钮
    public void OnBriberyButtonClick()
    {
        CoinArgs e = new CoinArgs()
        {
            coin = BriberyTime * 500
        };
        SendEvent(Consts.E_BriberyClickEventName, e);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        briberyText.text = (500 * BriberyTime).ToString();
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        BriberyTime = 1;
    }
}
