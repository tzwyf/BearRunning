using UnityEngine;
using System.Collections;

public class BuySkinController : Controller
{
    public override void Execute(object data)
    {
        BuySkinArgs e = data as BuySkinArgs;
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();
        if (gm.IsMoneyEnough(e.coin))
        {
            gm.m_playerClothesBought.Add(e.id);
            //更新UI
            shop.UpdateUI();
        }
    }
}
