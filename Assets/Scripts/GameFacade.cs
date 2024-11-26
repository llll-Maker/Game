using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance;//单例
    //提供子系统模板的对象引用
    public IAssetFactory assetFactory;
    private CameraControl cameraControl;
    private AchievementSystem achievementSystem;
    private void Awake()
    {
        Instance = this;
        achievementSystem = new AchievementSystem();    
        assetFactory=new ResourcesAssetProxy();
        cameraControl=Camera.main.GetComponent<CameraControl>();    
    }
    public void FindPlayer()
    {
        cameraControl.FindPlayer();
    }
    //提供创建备忘录的方法
    public void CreateMemeto(int currentScore)
    {
        achievementSystem.CreateMemeto(currentScore);
    }
    //提供资源加载的方法
    public GameObject LoadCharacter(string name)
    {
        return assetFactory.LoadCharacter(name);
    }

    public AudioClip LoadAudio(string name,int role)
    {
        return assetFactory.LoadAudio(name,role);
    }

    public Sprite LoadSprite(string name)
    {
        return assetFactory.LoadSprite(name);
    }
}
