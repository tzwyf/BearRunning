using UnityEngine;
using System.Collections;

public class BriberyClickController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        CoinArgs e = data as CoinArgs;
        UIDead dead = GetView<UIDead>();

        //钱够的话才能进行贿赂
        if (gm.IsMoneyEnough(e.coin))
        {
            dead.BriberyTime++;
            dead.Hide();

            //继续开始游戏前的读秒
            UIResume resume = GetView<UIResume>();
            resume.StartCount();
        }
    }
}
