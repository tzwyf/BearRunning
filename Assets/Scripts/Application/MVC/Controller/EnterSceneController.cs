using UnityEngine;
using System.Collections;

public class EnterSceneController : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        GameModel gm = GetModel<GameModel>();
        switch (e.sceneIndex)
        {
            case 1:
                Game.Instance.sound.PlayBg("Bgm_JieMian");
                RegisterView(GameObject.Find("Canvas").transform.Find("UIMainMenu").GetComponent<UIMainMenu>());
                break;
            case 2:
                Game.Instance.sound.PlayBg("Bgm_JieMian");
                RegisterView(GameObject.Find("Canvas").transform.Find("UIShop").GetComponent<UIShop>());
                break;
            case 3:
                Game.Instance.sound.PlayBg("Bgm_JieMian");
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBuyTools").GetComponent<UIBuyTools>());
                break;
            case 4:
                Game.Instance.sound.PlayBg("Bgm_ZhanDou");
                RegisterView(GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerMove>());
                RegisterView(GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerAnim>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIPause").GetComponent<UIPause>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIResume").GetComponent<UIResume>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIDead").GetComponent<UIDead>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIFinalScore").GetComponent<UIFinalScore>());

                gm.IsPause = false;
                gm.IsPlay = true;
                //Time.timeScale = 1;
                break;
        }
    }
}
