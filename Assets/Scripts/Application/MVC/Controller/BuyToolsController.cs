using UnityEngine;
using System.Collections;

public class BuyToolsController : Controller
{
    public override void Execute(object data)
    {
        BuyToolsArgs e = data as BuyToolsArgs;
        GameModel gm = GetModel<GameModel>();
        UIBuyTools buyTools = GetView<UIBuyTools>();

        switch (e.itemKind)
        {
            case ItemKind.InvincibleItem:
                if (gm.IsMoneyEnough(e.coin))
                {
                    gm.Invincible++;
                    buyTools.UpdateUI();
                }
                break;
            case ItemKind.MultiplyItem:
                if (gm.IsMoneyEnough(e.coin))
                {
                    gm.Multiply++;
                    buyTools.UpdateUI();
                }
                break;
            case ItemKind.MagnetItem:
                if (gm.IsMoneyEnough(e.coin))
                {
                    gm.Magnet++;
                    buyTools.UpdateUI();
                }
                break;
        }
    }
}