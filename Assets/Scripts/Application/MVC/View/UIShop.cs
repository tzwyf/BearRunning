using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    public MeshRenderer ballRenderer;
    public SkinnedMeshRenderer playerSkinRenderer;
    public Image footBallBuyButtonImage;
    public Image skinBuyButtonImage;
    public Image clothesBuyButtonImage;
    public List<Image> footballGizmoStateList = new List<Image>();
    public List<Image> skinGizmoStateList = new List<Image>();
    public List<Image> clothesGizmoStateList = new List<Image>();

    //图片资源
    public Sprite buySprite;
    public Sprite equipSprite;
    public Sprite unBuyGizmo;
    public Sprite buyGizmo;
    public Sprite equipGizmo;

    private int selectIndex;
    private ItemState state;

    //更新UI
    public List<Sprite> headSprites = new List<Sprite>();
    public Image headImage;
    public Text nameText;
    public Text moneyText;
    public Text gradeText;

    private GameModel gm;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_ShopName;
        }
    }
    #endregion

    #region 方法
    public void UpdateUI()
    {
        moneyText.text = gm.Coin.ToString();
        switch (gm.PlayerClothesEquipped.skinId)
        {
            case 0:
                nameText.text = "MoMo";
                break;
            case 1:
                nameText.text = "SaLi";
                break;
            case 2:
                nameText.text = "Sugar";
                break;
        }
        headImage.overrideSprite = headSprites[gm.PlayerClothesEquipped.skinId];
    }

    #region 足球
    public void NormalFootballClick()
    {
        selectIndex = 0;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(selectIndex).footballMat;
        UpdateBuyFootballButtonState(selectIndex);
    }
    public void ColorFootballClick()
    {
        selectIndex = 1;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(selectIndex).footballMat;
        UpdateBuyFootballButtonState(selectIndex);
    }
    public void FireFootballClick()
    {
        selectIndex = 2;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(selectIndex).footballMat;
        UpdateBuyFootballButtonState(selectIndex);
    }

    //根据索引更新足球购买按钮状态
    public void UpdateBuyFootballButtonState(int i)
    {
        state = gm.GetFootBallState(i);
        switch (state)
        {
            case ItemState.UnBuy:
                footBallBuyButtonImage.gameObject.SetActive(true);
                footBallBuyButtonImage.overrideSprite = buySprite;
                break;
            case ItemState.Buy:
                footBallBuyButtonImage.gameObject.SetActive(true);
                footBallBuyButtonImage.overrideSprite = equipSprite;
                break;
            case ItemState.Equip:
                footBallBuyButtonImage.gameObject.SetActive(false);
                break;
        }
    }
    //足球购买按钮点击
    public void OnBuyFootballClick()
    {
        state = gm.GetFootBallState(selectIndex);
        switch (state)
        {
            case ItemState.UnBuy:
                //购买
                BuyFootballArgs e = new BuyFootballArgs()
                {
                    coin = Game.Instance.staticData.GetFootballInfo(selectIndex).coin,
                    index = selectIndex
                };
                //发送事件由controller来处理
                SendEvent(Consts.E_BuyFootballEventName, e);
                break;
            case ItemState.Buy:
                //装备
                BuyFootballArgs e2 = new BuyFootballArgs()
                {
                    index = selectIndex
                };
                //发送事件由controller来处理
                SendEvent(Consts.E_EquipFootballEventName, e2);
                break;
            case ItemState.Equip:
                break;
        }
    }
    //更新足球右上角Gizmo
    public void UpdateFootballGozmo()
    {
        for (int i = 0; i < 3; i++)
        {
            ItemState state = gm.GetFootBallState(i);
            switch (state)
            {
                case ItemState.UnBuy:
                    footballGizmoStateList[i].overrideSprite = unBuyGizmo;
                    break;
                case ItemState.Buy:
                    footballGizmoStateList[i].overrideSprite = buyGizmo;
                    break;
                case ItemState.Equip:
                    footballGizmoStateList[i].overrideSprite = equipGizmo;
                    break;
            }
        }
    }
    #endregion

    #region 皮肤
    public void OnMomoClick()
    {
        selectIndex = 0;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(selectIndex, 0).footballMat;
        UpdateBuySkinButton();
    }
    public void OnSaliClick()
    {
        selectIndex = 1;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(selectIndex, 0).footballMat;
        UpdateBuySkinButton();
    }
    public void OnSugarClick()
    {
        selectIndex = 2;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(selectIndex, 0).footballMat;
        UpdateBuySkinButton();
    }
    //根据索引更新皮肤购买按钮状态
    public void UpdateBuySkinButton()
    {
        state = gm.GetPlayerSkinState(selectIndex);
        switch (state)
        {
            case ItemState.UnBuy:
                skinBuyButtonImage.gameObject.SetActive(true);
                skinBuyButtonImage.overrideSprite = buySprite;
                break;
            case ItemState.Buy:
                skinBuyButtonImage.gameObject.SetActive(true);
                skinBuyButtonImage.overrideSprite = equipSprite;
                break;
            case ItemState.Equip:
                skinBuyButtonImage.gameObject.SetActive(false);
                break;
        }
    }
    //皮肤购买按钮点击
    public void OnSkinBuyButtonClick()
    {
        state = gm.GetPlayerSkinState(selectIndex);
        switch (state)
        {
            case ItemState.UnBuy:
                //购买
                BuySkinArgs e = new BuySkinArgs()
                {
                    coin = Game.Instance.staticData.GetPlayerClothesInfo(selectIndex, 0).coin,
                    id = new BuyID { skinId=selectIndex,clothesId=0}
                };
                //发送事件由controller来处理
                SendEvent(Consts.E_BuySkinEventName, e);
                break;
            case ItemState.Buy:
                //装备
                BuySkinArgs e2 = new BuySkinArgs()
                {
                    id = new BuyID { skinId = selectIndex, clothesId = 0 }
                };
                //发送事件由controller来处理
                SendEvent(Consts.E_EquipSkinEventName, e2);
                break;
            case ItemState.Equip:
                break;
        }
        UpdateBuySkinButton();
        UpdateSkinGozmo();
    }

    //更新皮肤右上角Gizmo
    public void UpdateSkinGozmo()
    {
        for (int i = 0; i < 3; i++)
        {
            state = gm.GetPlayerSkinState(i);
            switch (state)
            {
                case ItemState.UnBuy:
                    skinGizmoStateList[i].overrideSprite = unBuyGizmo;
                    break;
                case ItemState.Buy:
                    skinGizmoStateList[i].overrideSprite = buyGizmo;
                    break;
                case ItemState.Equip:
                    skinGizmoStateList[i].overrideSprite = equipGizmo;
                    break;
            }
        }
    }
    #endregion

    #region 衣服
    public void OnNormalClick()
    {
        selectIndex = 0;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, selectIndex).footballMat;
        UpdateBuyClothesButton();
    }
    public void OnBrazilClick()
    {
        selectIndex = 1;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, selectIndex).footballMat;
        UpdateBuyClothesButton();
    }
    public void OnGermanyClick()
    {
        selectIndex = 2;
        Game.Instance.sound.PlayEffect("Se_UI_Dress");
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, selectIndex).footballMat;
        UpdateBuyClothesButton();
    }

    //根据索引更新衣服购买按钮状态
    public void UpdateBuyClothesButton()
    {
        BuyID ID = new BuyID
        {
            skinId = gm.PlayerClothesEquipped.skinId,
            clothesId = selectIndex
        };
        state = gm.GetPlayerClothesState(ID);
        switch (state)
        {
            case ItemState.UnBuy:
                clothesBuyButtonImage.gameObject.SetActive(true);
                clothesBuyButtonImage.overrideSprite = buySprite;
                break;
            case ItemState.Buy:
                clothesBuyButtonImage.gameObject.SetActive(true);
                clothesBuyButtonImage.overrideSprite = equipSprite;
                break;
            case ItemState.Equip:
                clothesBuyButtonImage.gameObject.SetActive(false);
                break;
        }
    }

    //衣服购买按钮点击
    public void OnClothesBuyButtonClick()
    {
        BuyID ID = new BuyID
        {
            skinId = gm.PlayerClothesEquipped.skinId,
            clothesId = selectIndex
        };
        state = gm.GetPlayerClothesState(ID);
        switch (state)
        {
            case ItemState.UnBuy:
                //购买
                BuySkinArgs e = new BuySkinArgs()
                {
                    coin = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, selectIndex).coin,
                    id = ID
                };
                //发送事件由controller来处理
                SendEvent(Consts.E_BuySkinEventName, e);
                break;
            case ItemState.Buy:
                //装备
                BuySkinArgs e2 = new BuySkinArgs()
                {
                    id = ID
                };
                //发送事件由controller来处理
                SendEvent(Consts.E_EquipSkinEventName, e2);
                break;
            case ItemState.Equip:
                break;
        }
        UpdateBuyClothesButton();
        UpdateClothesGozmo();
    }

    //更新衣服右上角Gizmo
    public void UpdateClothesGozmo()
    {
        for (int i = 0; i < 3; i++)
        {
            BuyID ID = new BuyID
            {
                skinId = gm.PlayerClothesEquipped.skinId,
                clothesId = i
            };
            state = gm.GetPlayerClothesState(ID);
            switch (state)
            {
                case ItemState.UnBuy:
                    clothesGizmoStateList[i].overrideSprite = unBuyGizmo;
                    break;
                case ItemState.Buy:
                    clothesGizmoStateList[i].overrideSprite = buyGizmo;
                    break;
                case ItemState.Equip:
                    clothesGizmoStateList[i].overrideSprite = equipGizmo;
                    break;
            }
        }
    }

    #endregion

    public void OnHeadButtonClick()
    {
        UpdateSkinGozmo();
        HideAllBuyButton();
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, 
            gm.PlayerClothesEquipped.clothesId).footballMat;
    }
    public void OnClothesButtonClick()
    {
        UpdateClothesGozmo();
        HideAllBuyButton();
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId,
            gm.PlayerClothesEquipped.clothesId).footballMat;
    }
    public void OnFootballButtonClick()
    {
        UpdateFootballGozmo();
        HideAllBuyButton();
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId,
            gm.PlayerClothesEquipped.clothesId).footballMat;
    }
    private void HideAllBuyButton()
    {
        footBallBuyButtonImage.gameObject.SetActive(false);
        skinBuyButtonImage.gameObject.SetActive(false);
        clothesBuyButtonImage.gameObject.SetActive(false);
    }


    public void OnStartGameButtonClick()
    {
        Game.Instance.LoadLevel(3);
    }
    public void OnReturnButtonClick()
    {
        if (gm.lastIndex == 4)
        {
            gm.lastIndex = 1;
        }
        Game.Instance.LoadLevel(gm.lastIndex);
    }
    #endregion



    #region Unity回调
    private void Awake()
    {
        gm = GetModel<GameModel>();
        UpdateUI();
        
        UpdateSkinGozmo();
        playerSkinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, gm.PlayerClothesEquipped.clothesId).footballMat;
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
        gradeText.text = gm.Grade.ToString();
    }
    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {
        
    }
    #endregion

    #region 帮助方法
    #endregion



}
