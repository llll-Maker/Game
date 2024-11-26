using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 备忘录发起人
/// </summary>
public class AchievementSystem 
{
    private AchievementMemento memento;//获取备忘录对象

    private int score1, score2, score3;

    public AchievementSystem()
    {
        memento = new AchievementMemento();
        SetMemeto(memento);
    }

    //创建备忘录，数据保存
    public void CreateMemeto(int currentScore)
    {
        //冒泡排序法
        int[] scoreData = new int[] { score1, score2, score3, currentScore };
        for (int i = 0; i < scoreData.Length - 1; i++)
        {
            for (int j = 0; j < scoreData.Length - 1 - i; j++)
            {
                if (scoreData[j] > scoreData[j + 1])
                {
                    int temp = scoreData[j + 1];
                    scoreData[j + 1] = scoreData[j];
                    scoreData[j] = temp;
                }
            }
        }
        //完成冒泡之后，获得从小到大的排名
        memento.score1 = scoreData[1];
        memento.score2 = scoreData[2];
        memento.score3 = scoreData[3];
        //时间越短越好
        //调用备忘录里面数据保存的函数 使用数据库或者文件流/本地化持久保存
        memento.SaveData();
    }
    //读取数据
    private void SetMemeto(AchievementMemento memento)
    {
        score1 = memento.score1;
        score2 = memento.score2;
        score3 = memento.score3;
    }
}
