using UnityEngine;
using System.Collections;

public class FinalUIController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();

        UIDead dead = GetView<UIDead>();
        dead.Hide();

        UIBoard board = GetView<UIBoard>();
        //将游戏内获得的金币加入总金币中
        gm.Coin += board.Coin;
        board.Hide();

        UIFinalScore final = GetView<UIFinalScore>();
        final.Show();
        //更新经验exp
        gm.Exp = board.Coin + board.Distance + board.GoalCount;
        //更新UI
        final.UpdateUI(board.Distance, board.Coin, board.GoalCount,gm.Grade,gm.Exp);
    }
}
