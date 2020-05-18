using UnityEngine;
using System.Collections;
/// <summary>
/// 读秒结束后的操作
/// </summary>
public class ContinueGameController : Controller
{
    public override void Execute(object data)
    {
        GameModel gm = GetModel<GameModel>();
        gm.IsPause = false;
        gm.IsPlay = true;
        UIBoard board = GetView<UIBoard>();
        //如果是时间用完然后贿赂继续游戏的，+20秒续命
        if(board.Timer < 0.1)
        {
            board.Timer += 20;
        }
    }
}
