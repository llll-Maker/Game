using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesAssetProxy : IAssetFactory
{
    private ResourcesAssetFactory assetFactory=new ResourcesAssetFactory();
    //用字典数据保存资源
    Dictionary<string, GameObject> mCharacter = new Dictionary<string, GameObject>();
    Dictionary<string, Sprite> mSprite = new Dictionary<string, Sprite>();
    Dictionary<string, AudioClip> mSound = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudio(string name,int role)
    {
        if (mSound.ContainsKey(name))
        {
            //资源存在，不需要加载，直接生成
            return mSound[name];
        }
        else
        {
            //资源不存在，首次加载，需要添加到字典数据里
            AudioClip asset = assetFactory.LoadAudio(name,role) as AudioClip;
            mSound.Add(name, asset);
            return asset;
        }
    }

    public GameObject LoadCharacter(string name)
    {
        if (mCharacter.ContainsKey(name))
        {
            //资源存在，不需要加载，直接生成
            return GameObject.Instantiate(mCharacter[name]);
        }
        else
        {
            //资源不存在，首次加载，需要添加到字典数据里
            GameObject asset = assetFactory.LoadAsset(assetFactory.CharacterPath + name) as GameObject;
            mCharacter.Add(name, asset);
            return GameObject.Instantiate(asset);
        }
    }

    public Sprite LoadSprite(string name)
    {
        if (mSprite.ContainsKey(name))
        {
            //资源存在，不需要加载，直接生成
            return mSprite[name];
        }
        else
        {
            //资源不存在，首次加载，需要添加到字典数据里
            Sprite asset = assetFactory.LoadSprite(name) as Sprite;
            mSprite.Add(name, asset);
            return asset;
        }
    }
}
