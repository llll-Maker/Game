using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetFactory
{
    GameObject LoadCharacter(string name);
    AudioClip LoadAudio(string name,int role);   
    Sprite LoadSprite(string name);
}
   

