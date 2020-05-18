using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpController : Controller
{
    public override void Execute(object data)
    {
        //注册所有的Controller
        RegisterController(Consts.E_EnterSceneEventName, typeof(EnterSceneController));
        RegisterController(Consts.E_EndGameEventName, typeof(EndGameController));
        RegisterController(Consts.E_PauseGameEventName, typeof(PauseGameController));
        RegisterController(Consts.E_ResumeGameEventName, typeof(ResumeGameController));
        RegisterController(Consts.E_HitItemEventName, typeof(HitItemController));
        RegisterController(Consts.E_FinalUIEventName, typeof(FinalUIController));
        RegisterController(Consts.E_BriberyClickEventName, typeof(BriberyClickController));
        RegisterController(Consts.E_ContinueGameEventName, typeof(ContinueGameController));
        RegisterController(Consts.E_BuyToolsEventName, typeof(BuyToolsController));
        RegisterController(Consts.E_BuyFootballEventName, typeof(BuyFootballController));
        RegisterController(Consts.E_EquipFootballEventName, typeof(EquipFootballController));
        RegisterController(Consts.E_BuySkinEventName, typeof(BuySkinController));
        RegisterController(Consts.E_EquipSkinEventName, typeof(EquipSkinController));

        //注册Model
        RegisterModel(new GameModel());

        //初始化
        GameModel gm = GetModel<GameModel>();
        gm.Init();
    }
}
