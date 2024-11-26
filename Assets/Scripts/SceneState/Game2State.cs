using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2State : ISceneState
{
    private Transform Set;
    private Transform WinBg;
    private Transform DefeatBg;
    int i = 1;
    public Game2State(SceneStateController controller) : base("game2", controller)
    {
    }
    public override void StateStart()
    {
       // if (i < 1) return;
        base.StateStart();
        Init();
        GameFacade.Instance.FindPlayer();
        Set = GameObject.Find("Canvas/Set").transform;
        WinBg = GameObject.Find("Canvas").transform.GetChild(11);
        DefeatBg = GameObject.Find("Canvas").transform.GetChild(12);
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
            controller.SetState(new Game2State(controller));

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
            controller.SetState(new Game2State(controller));
        });
        WinBg.GetChild(7).GetComponent<Button>().onClick.AddListener(() =>
        {
            //回到登录界面
            controller.SetState(new StartState(controller));
        });
        //失败面板
        DefeatBg.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            DefeatBg.gameObject.SetActive(false);//关闭面板
        });
        DefeatBg.GetChild(5).GetComponent<Button>().onClick.AddListener(() =>
        {
            //回到登录界面
            controller.SetState(new Game1State(controller));
        });
        DefeatBg.GetChild(6).GetComponent<Button>().onClick.AddListener(() =>
        {
            controller.SetState(new Game2State(controller));
        });
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
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
       // i--;
    }
}
