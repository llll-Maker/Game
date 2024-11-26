using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class LoadState : ISceneState
{
    public LoadState(SceneStateController controller) : base("Loading", controller)
    {
    }
    private Image LoadImage;
    float waitTime = 0;
    float allTime = 13;
    public override void StateStart()
    {
        //加载这个场景所需做的事情
        base.StateStart();
        LoadImage = GameObject.Find("loadingbar").GetComponent<Image>();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        waitTime += Time.deltaTime;
        LoadImage.fillAmount = waitTime / allTime;//(0-1)的数字
        if (waitTime >= allTime)
        {
            //切换状态
            controller.SetState(new AnimationState(controller));//会自己进入游戏场景
        }
    }
}
