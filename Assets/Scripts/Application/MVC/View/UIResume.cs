using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResume : View
{
    public Image countImage;
    public Sprite[] countSprites;

    public override string Name
    {
        get
        {
            return Consts.V_ResumeName;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public void StartCount()
    {
        Show();
        StartCoroutine(StartCountCor());
    }

    IEnumerator StartCountCor()
    {
        int i = 3;
        while (i > 0)
        {
            countImage.sprite = countSprites[i - 1];
            i--;
            yield return new WaitForSeconds(1);
            if (i <= 0)
            {
                break;
            }
        }

        Hide();
        //Time.timeScale = 1;
        //此处需要发送一个事件，然后定义一个controller来完成该操作
        //GameModel gm = GetModel<GameModel>();
        //gm.IsPause = false;
        //gm.IsPlay = true;
        SendEvent(Consts.E_ContinueGameEventName);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
