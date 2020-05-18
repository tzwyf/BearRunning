using UnityEngine;
using System.Collections;

public class EquipFootballController : Controller
{
    public override void Execute(object data)
    {
        BuyFootballArgs e = data as BuyFootballArgs;
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();

        gm.FootballEquipped = e.index;
        //更新状态
        shop.UpdateBuyFootballButtonState(e.index);
        shop.UpdateFootballGozmo();
    }
}
