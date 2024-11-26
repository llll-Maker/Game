using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1State : ISceneState
{
    private Transform  Set;
    private Transform WinBg;
    private Transform DefeatBg;
    //static float timer = 0;//进行计时的值类型

    //bool gameEnd = false;//检测游戏结束

    public Game1State(SceneStateController controller) : base("game1", controller)
    {
    }

    public override void StateStart()
    {
        base.StateStart();
        Init();
        Set = GameObject.Find("Canvas/Set").transform;
        WinBg = GameObject.Find("Canvas").transform.GetChild(11);
        DefeatBg = GameObject.Find("Canvas").transform.GetChild(12);
        GameFacade.Instance.FindPlayer();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
        }
        Set.GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 0;
            Set.transform.GetChild(0).gameObject.SetActive(true);
        });
        Set.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            Set.transform.GetChild(0).gameObject.SetActive(false);
        });
        Set.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            //按下重新开始游戏按钮
            controller.SetState(new Game1State(controller));
        });
        Set.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            //按下回到游戏按钮
            Time.timeScale = 1f;
            Set.transform.GetChild(0).gameObject.SetActive(false);
        });
        Set.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        {

            //退出游戏
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        });

        //成功面板
        WinBg.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            WinBg.gameObject.SetActive(false);//关闭面板
        });
        WinBg.GetChild(6).GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.SetState(new Game1State(controller));
        }); 
        WinBg.GetChild(7).GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.SetState(new Game2State(controller));
        });
        //失败面板
        DefeatBg.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            DefeatBg.gameObject.SetActive(false);//关闭面板
        });
        DefeatBg.GetChild(5).GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.SetState(new StartState(controller));
        });
        DefeatBg.GetChild(6).GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.SetState(new Game1State(controller));
        });
        //EventCenter.AddListerner(EventNum.Defeat1, StopTimer);
        //EventCenter.AddListerner(EventNum.Win1, StopTimer1);
    }
    
    public override void StateUpdate()
    {
        //if (gameEnd) return;
        base.StateUpdate();
        //开始计时
        //timer += Time.deltaTime;
        //GameObject.Find("Canvas/Time/time").GetComponent<Text>().text = ((int)timer).ToString();
    }
    private void StopTimer1()
    {
        //停止计时
       // CharacterControl.instace.BroadWin();
       // gameEnd = true;
        //调用创建备忘录的方法
       // GameFacade.Instance.CreateMemeto((int)timer);
    }
    private void StopTimer()
    {
        //停止计时
        //CharacterControl.instace.BroadDeath();
       // gameEnd = true;
        //调用创建备忘录的方法
        //GameFacade.Instance.CreateMemeto((int)timer);
    }
    private void Init()
    {
        Debug.Log(PlayerPrefs.GetInt("Player"));
        switch (PlayerPrefs.GetInt("Player"))
        {
            case 0:
                GameFacade.Instance.LoadCharacter("Player");
                break;
            case 1:
                GameFacade.Instance.LoadCharacter("Player2");
                break;
            case 2:
                GameFacade.Instance.LoadCharacter("Player1");
                break;
            default:
                break;
        }
    }
    public override void StateEnd()
    {
        base.StateEnd();
        //EventCenter.RemoveListener(EventNum.Defeat1, StopTimer);
        //EventCenter.RemoveListener(EventNum.Win1, StopTimer1);
    }
}
   
