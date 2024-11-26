using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    //获取当前状态
    private ISceneState sceneState;
    //保存异步操作符
    private AsyncOperation mAO;
    //场景完成之后，资源不能重复初始化
    private bool mIsRunStart = false;
    //提供内部切换状态的行为
    public void SetState(ISceneState sceneState, bool isLoadScene = true)
    {
        //bool类型参数可以赋值，如果其他地放运用的时候不改值，则默认为true
        //判断初始场景是不是为空，如果不是第一次进入状态则清理上一个状态的数据
        if (this.sceneState != null)
        {
            this.sceneState.StateEnd();//第一次是不会清空的
        }
        //更新当前状态
        this.sceneState = sceneState;
        //开始加载场景，使用异步加载
        if (isLoadScene)
        {
            mAO = SceneManager.LoadSceneAsync(sceneState.SceneName);//实现异步加载
            mIsRunStart = false;//保证进行场景加载之前就是false
        }
        else
        {
            //不需要加载的就是第一次运行进入的开始场景，直接初始化即可
            sceneState.StateStart();//已经完成初始化
            mIsRunStart = true;//防止多次初始化
        }

    }
    public void StateUpdate()
    {
        //场景切换是否完成，如果没有完成则等待
        if (mAO != null && !mAO.isDone) return;//没有加载完成
        //异步加载完成，进行跳转
        if (mAO != null && mAO.isDone && mIsRunStart == false)
        {
            //初始化新场景里面的资源
            sceneState.StateStart();
            mIsRunStart = true;
        }
        //每一帧进行更新
        if (sceneState != null)
            sceneState.StateUpdate();
    }
}
