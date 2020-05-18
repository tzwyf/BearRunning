using UnityEngine;
using System.Collections;
/// <summary>
/// 读秒操作
/// </summary>
public class ResumeGameController : Controller
{
    public override void Execute(object data)
    {
        UIResume resume = GetView<UIResume>();
        resume.StartCount();
    }
}
