using UnityEngine;
using System.Collections;

public class ExitSceneController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.sceneIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Game.Instance.objectPool.Clear();
                break;
        }
        GameModel gm = GetModel<GameModel>();
        gm.lastIndex = e.sceneIndex;
    }
}
