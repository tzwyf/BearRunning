using UnityEngine;
using System.Collections;

public class EndGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPlay = false;

        //TODO:显示游戏结束的面板
        UIDead dead = GetView<UIDead>();
        dead.Show();
    }
}
