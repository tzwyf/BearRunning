using UnityEngine;
using System.Collections;

public class PauseGameController : Controller
{
    public override void Execute(object data)
    {
        PauseArgs e = data as PauseArgs;
        GameModel gm = GetModel<GameModel>();
        gm.IsPause = true;

        UIPause pause = GetView<UIPause>();
        pause.Show();

        pause.scoreText.text = e.score.ToString();
        pause.distanceText.text = e.distance.ToString();
        pause.moneyText.text = e.coin.ToString();

        //Time.timeScale = 0;
    }
}
