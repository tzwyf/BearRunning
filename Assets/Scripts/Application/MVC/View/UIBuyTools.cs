using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyTools : View
{
    private GameModel gm;
    public Text moneyText;
    public Text invincibleCountText;
    public Text magnetCountText;
    public Text multiplyCountText;

    public SkinnedMeshRenderer skinRenderer;
    public MeshRenderer ballRenderer;

    public override string Name
    {
        get
        {
            return Consts.V_BuyToolsName;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    private void Awake()
    {
        gm = GetModel<GameModel>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = gm.Coin.ToString();
        ShowOrHide(gm.Invincible, invincibleCountText);
        ShowOrHide(gm.Magnet, magnetCountText);
        ShowOrHide(gm.Multiply, multiplyCountText);

        skinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, gm.PlayerClothesEquipped.clothesId).footballMat;
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
    }

    private void ShowOrHide(int count, Text text)
    {
        if (count > 0)
        {
            text.transform.parent.gameObject.SetActive(true);
            text.text = count.ToString();
        }
        else
        {
            text.transform.parent.gameObject.SetActive(false);
        }
    }

    //购买按钮点击
    public void OnBuyInvincibleButtonClick(int i=100)
    {
        BuyToolsArgs e = new BuyToolsArgs()
        {
            itemKind = ItemKind.InvincibleItem,
            coin = i
        };
        SendEvent(Consts.E_BuyToolsEventName, e);
    }
    public void OnBuyMagnetButtonClick(int i = 200)
    {
        BuyToolsArgs e = new BuyToolsArgs()
        {
            itemKind = ItemKind.MagnetItem,
            coin = i
        };
        SendEvent(Consts.E_BuyToolsEventName, e);
    }
    public void OnBuyMultiplyButtonClick(int i = 200)
    {
        BuyToolsArgs e = new BuyToolsArgs()
        {
            itemKind = ItemKind.MultiplyItem,
            coin = i
        };
        SendEvent(Consts.E_BuyToolsEventName, e);
    }

    public void OnRandomBuyButtonClick(int i=300)
    {
        int num = Random.Range(0, 3);
        switch (num)
        {
            case 0:
                OnBuyInvincibleButtonClick(i);
                break;
            case 1:
                OnBuyMagnetButtonClick(i);
                break;
            case 2:
                OnBuyMultiplyButtonClick(i);
                break;
        }
    }

    public void OnStartGameButtonClick()
    {
        Game.Instance.LoadLevel(4);
    }
    public void OnReturnButtonClick()
    {
        if (gm.lastIndex == 4)
        {
            gm.lastIndex = 1;
        }
        Game.Instance.LoadLevel(gm.lastIndex);
    }
}
