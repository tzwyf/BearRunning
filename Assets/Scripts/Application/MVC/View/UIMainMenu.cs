using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : View
{
    public SkinnedMeshRenderer skinRenderer;
    public MeshRenderer ballRenderer;
    private GameModel gm;

    public override string Name
    {
        get
        {
            return Consts.V_MainMenuName;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public void OnShopButtonClick()
    {
        Game.Instance.LoadLevel(2);
    }
    public void OnStartGameButtonClick()
    {
        Game.Instance.LoadLevel(3);
    }

    private void Awake()
    {
        gm = GetModel<GameModel>();
        skinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, gm.PlayerClothesEquipped.clothesId).footballMat;
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
    }
}
