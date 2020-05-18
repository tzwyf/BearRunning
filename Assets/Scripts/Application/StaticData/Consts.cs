using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Consts {
    //事件名称
    public const string E_ExitSceneEventName = "E_ExitSceneEvent";//ScenesArgs
    public const string E_EnterSceneEventName = "E_EnterSceneEvent";//ScenesArgs
    public const string E_StartUpEventName = "E_StartUpEvent";
    public const string E_EndGameEventName = "E_EndGameEvent";
    public const string E_PauseGameEventName = "E_PauseGame";
    public const string E_ResumeGameEventName = "E_ResumeGame";
    public const string E_ContinueGameEventName = "E_ContinueGame";

    /// <summary>
    /// UI相关
    /// </summary>
    public const string E_UpdateDisEventName = "E_UpdateDis";//DistanceArgs
    public const string E_UpdateCoinEventName = "E_UpdateCoin";//CoinArgs
    public const string E_AddTimeEventName = "E_AddTime";
    public const string E_HitItemEventName = "E_HitItem";//ItemArgs
    public const string E_HitGoalTriggerEventName = "E_HitGoalTrigger";//UIBoard 可以射门了
    public const string E_ClickGoalButtonEventName = "E_ClickGoalButton";
    public const string E_ShootGoalEventName = "E_ShootGoal";
    public const string E_FinalUIEventName = "E_FinalUI";
    public const string E_BriberyClickEventName = "E_BriberyClick";//CoinArgs
    public const string E_BuyToolsEventName = "E_BuyTools";//BuyToolsArgs
    public const string E_BuyFootballEventName = "E_BuyFootball";//BuyFootballArgs
    public const string E_EquipFootballEventName = "E_EquipFootball";//BuyFootballArgs-index
    public const string E_BuySkinEventName = "E_BuySkin";//BuySkinArgs
    public const string E_EquipSkinEventName = "E_EquipSkin";//BuySkinArgs-id

    //视图名称
    public const string V_PlayerMoveName = "V_PlayerMove";
    public const string V_PlayerAnimName = "V_PlayerAnim";
    public const string V_BoardName = "V_Board";
    public const string V_PauseName = "V_Pause";
    public const string V_ResumeName = "V_Resume";
    public const string V_DeadName = "V_Dead";
    public const string V_FinalScoreName = "V_FinalScore";
    public const string V_BuyToolsName = "V_BuyTools";
    public const string V_MainMenuName = "V_MainMenu";
    public const string V_ShopName = "V_Shop";

    //模型名称
    public const string M_GameModel = "M_GameModel";
}

public enum InputDirection {
    Null,
    Right,
    Left,
    Up,
    Down
}

public enum ItemKind
{
    InvincibleItem,
    MultiplyItem,
    MagnetItem
}

public enum ItemState
{
    UnBuy,
    Buy,
    Equip
}
