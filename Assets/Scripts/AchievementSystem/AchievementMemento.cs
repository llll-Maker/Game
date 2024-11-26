using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//备忘录 存储和读取背包里面的数据
//备忘录数据只能在发起人那里调用
public class AchievementMemento 
{
    //前三名数据
    public int score1 { get; set; }
    public int score2 { get; set; }
    public int score3 { get; set; }
    public AchievementMemento()
    {
        //对数据加载起来，方便和本次游戏数据进行比对
        LoadData();
    }
    //更新数据
    public void SaveData()
    {
        //数据保存的入口，使用数据库或者文件流/本地化持久保存
        PlayerPrefs.SetInt("SCORE1", score1);
        PlayerPrefs.SetInt("SCORE2", score2);
        PlayerPrefs.SetInt("SCORE3", score3);
    }
    //加载数据
    public void LoadData()
    {
        //数据加载的入口，使用数据库或者文件流/本地化持久保存
        score1 = PlayerPrefs.GetInt("SCORE1");
        score2 = PlayerPrefs.GetInt("SCORE2");
        score3 = PlayerPrefs.GetInt("SCORE3");
    }
}
