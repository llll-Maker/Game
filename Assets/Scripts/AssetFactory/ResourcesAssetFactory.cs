using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ResourcesAssetFactory : IAssetFactory
{
    //提供加载路径
    public string CharacterPath = "Character/";
    public string AudioPath = "Sound/";
    public string AudioPath1 = "Sound/Player/";
    public string AudioPath2 = "Sound/Player1/";
    public string SpritePath = "Sprite/";
    public AudioClip LoadAudio(string name,int role)
    {
        string audioPath =AudioPath;
        switch (role)
        { 
            case 0:
                audioPath = AudioPath1;
                break;
            case 1: 
                audioPath = AudioPath1;
                break;
            case 2: 
                audioPath = AudioPath2;
                break;
            case 3:
                audioPath = AudioPath;
                break;
            default:
                break;
        }
        Debug.Log(audioPath + name);
        return Resources.Load<AudioClip>(audioPath + name);
    }

    public GameObject LoadCharacter(string name)
    {
        return InstantiateGameObject(CharacterPath + name);
    }

    public Sprite LoadSprite(string name)
    {
        return Resources.Load<Sprite>(SpritePath + name);
    }
    //模板方法
    private GameObject InstantiateGameObject(string path)
    {
        Object o = Resources.Load(path);//加载资源
        if (o == null)
        {
            Debug.Log("加载不出来，请检查路径" + path);
            return null;
        }
        return GameObject.Instantiate(o) as GameObject;//实例话成3d物体
    }

    //加载资源的函数
    public Object LoadAsset(string path)
    {
        Object o = Resources.Load(path);
        if (o == null)
        {
            Debug.LogError("无法加载资源，路径：" + path);
            return null;
        }
        return o;
    }
}
