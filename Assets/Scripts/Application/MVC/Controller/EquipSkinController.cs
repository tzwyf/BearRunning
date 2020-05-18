using UnityEngine;
using System.Collections;

public class EquipSkinController : Controller
{
    public override void Execute(object data)
    {
        BuySkinArgs e = data as BuySkinArgs;
        GameModel gm = GetModel<GameModel>();
        UIShop shop = GetView<UIShop>();

        gm.PlayerClothesEquipped = e.id;
        //更新UI
        shop.UpdateUI();
    }
}
