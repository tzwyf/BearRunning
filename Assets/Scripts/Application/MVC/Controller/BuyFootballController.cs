using UnityEngine;
using System.Collections;

public class BuyFootballController : Controller
{
    public override void Execute(object data)
    {
        BuyFootballArgs e = data as BuyFootballArgs;
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        if (gm.IsMoneyEnough(e.coin))
        {
            gm.m_footballBought.Add(e.index);
            //更新状态
            shop.UpdateBuyFootballButtonState(e.index);
            shop.UpdateFootballGozmo();
            //更新UI
            shop.UpdateUI();
        }
    }
}
