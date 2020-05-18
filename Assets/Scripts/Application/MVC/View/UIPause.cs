using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : View
{
    public Text scoreText;
    public Text moneyText;
    public Text distanceText;

    public SkinnedMeshRenderer skinRenderer;
    public MeshRenderer ballRenderer;
    private GameModel gm;

    public override string Name
    {
        get
        {
            return Consts.V_PauseName;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    //继续按钮点击
    public void OnResumeButtonClick()
    {
        Hide();
        SendEvent(Consts.E_ResumeGameEventName);
    }
    //返回主页面
    public void OnHomeButtonClick()
    {
        Game.Instance.LoadLevel(1);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
        UpdateSkin();
    }

    private void Awake()
    {
        UpdateSkin();
    }

    private void UpdateSkin()
    {
        gm = GetModel<GameModel>();
        skinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, gm.PlayerClothesEquipped.clothesId).footballMat;
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
    }
}
