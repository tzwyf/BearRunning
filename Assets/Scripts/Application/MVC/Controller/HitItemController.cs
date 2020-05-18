using UnityEngine;
using System.Collections;

public class HitItemController : Controller
{
    public override void Execute(object data)
    {
        ItemArgs e = data as ItemArgs;
        PlayerMove playerMove = GetView<PlayerMove>();
        UIBoard uIBoard = GetView<UIBoard>();
        GameModel gm = GetModel<GameModel>();

        switch (e.kind)
        {
            case ItemKind.InvincibleItem:
                gm.Invincible -= e.hitCount;
                playerMove.HitWhist();
                uIBoard.HitWhist();
                break;
            case ItemKind.MultiplyItem:
                gm.Multiply -= e.hitCount;
                playerMove.HitMultiply();
                uIBoard.HitMultiply();
                break;
            case ItemKind.MagnetItem:
                gm.Magnet -= e.hitCount;
                playerMove.HitMagnet();
                uIBoard.HitMagnet();
                break;
            default:
                break;
        }

        uIBoard.UpdateUI();
    }
}
