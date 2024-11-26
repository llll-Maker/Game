using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class StartState : ISceneState
{
    public StartState(SceneStateController controller) : base("Start", controller)
    {
    }
    private Transform UIRoot;
    private Transform GamePanel;
    private Transform Card;

    public override void StateStart()
    {
        base.StateStart();
        UIRoot = GameObject.Find("Canvas").transform;
        GamePanel = UIRoot.Find("游戏界面");
        Card = GamePanel.Find("Card");
        RemeberPlayer(0);
        RemeberPlayer(1);
        RemeberPlayer(2);
        
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    private void RemeberPlayer(int i)
    {
        Card.GetChild(i).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Player", i);//根据保存i的值，到时候在游戏场景中加载出不同的人来
            controller.SetState(new LoadState(controller));//跳转到加载场景中
            Debug.Log(PlayerPrefs.GetInt("Player"));
        });
    }
}
