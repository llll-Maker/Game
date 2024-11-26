using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使用状态模式管理场景
/// </summary>
public class ISceneState 
{
    //需要加载的场景名字
    private string sceneName;
    //场景状态的管理者
    protected SceneStateController controller;

    public string SceneName { get => sceneName; set => sceneName = value; }
    public ISceneState(string sceneName, SceneStateController controller)
    {
        SceneName =sceneName;
        this.controller = controller;
    }
    //进入状态的时候调用（初始化）
    public virtual void StateStart() { }
    //离开状态的时候调用（资源回收）
    public virtual void StateEnd() { }
    //在这个状态下每一帧调用
    public virtual void StateUpdate() { }
}
