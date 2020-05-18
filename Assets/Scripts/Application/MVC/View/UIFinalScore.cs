using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalScore : View
{
    public Text scoreText;
    public Text moneyText;
    public Text distanceText;
    public Text goalText;
    public Text expText;
    public Text gradeText;
    public Slider expSlider;

    public override string Name
    {
        get
        {
            return Consts.V_FinalScoreName;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void UpdateUI(float distance,int coin,float goalCount,int grade,float exp)
    {
        distanceText.text = distance.ToString();
        moneyText.text = coin.ToString();
        goalText.text = goalCount.ToString();
        scoreText.text = (coin + distance * (goalCount + 1)).ToString();
        gradeText.text = grade.ToString() + "级";
        expText.text = exp.ToString() + "/" + (500 + grade * 100).ToString();
        expSlider.value = exp / (500 + grade * 100);
    }

    //点击重玩按钮
    public void OnReplayButtonClick()
    {
        Game.Instance.LoadLevel(4);
    }
    //返回主页面
    public void OnHomeButtonClick()
    {
        Game.Instance.LoadLevel(1);
    }
    //返回Shop
    public void OnShopButtonClick()
    {
        Game.Instance.LoadLevel(2);
    }
}
