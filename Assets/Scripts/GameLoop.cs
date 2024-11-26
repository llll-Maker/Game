using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //获得状态管理对象的引用
    private SceneStateController stateController;
    private void Awake()
    {
        //避免出现重复的两个物体
        if (GameObject.Find("GameLoop").gameObject != this.gameObject)
            Destroy(this.gameObject);
        //场景跳转，不销毁物体(物体一直存在）
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        stateController = new SceneStateController();
        //进入开始场景不需要加载，直接调用状态的初始化
        stateController.SetState(new StartState(stateController), false);

    }
  
    private void Update()
    {
        if (stateController != null)
            stateController.StateUpdate();

    }
}
