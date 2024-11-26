using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//事件码
public enum EventNum
{
    NONE,Defeat,Win
}
//定义一个委托
public delegate void CallBack();
public class EventCenter : MonoBehaviour
{
    //定义字典数据，键值对存储事件码和委托
    private static Dictionary<EventNum, Delegate> mEventDic = new Dictionary<EventNum, Delegate>();

    //提供观察者监听被观察者的行为变化
    public static void AddListerner(EventNum eventNum, CallBack callBack)
    {
        //添加监听之前，先判断字典里面有没有我们需要的事件码
        if (!mEventDic.ContainsKey(eventNum))
        {
            mEventDic.Add(eventNum, null);
        }
        //不然条件不能通用，加了两次callBack
        mEventDic[eventNum] = (CallBack)mEventDic[eventNum] + callBack;
    }
    //提供观察者移除监听被观察者的行为变化
    public static void RemoveListener(EventNum eventNum, CallBack callBack)
    {
        //移除监听之前，先判断字典里面有没有委托需要移除，委托全部移除完，在移除键
        mEventDic[eventNum] = (CallBack)mEventDic[eventNum] - callBack;
        if (mEventDic[eventNum] == null)
            mEventDic.Remove(eventNum);
    }
    //提过给被观察者广播事件的方法
    public static void Broadcast(EventNum eventNum)
    {
        Delegate d;
        if (mEventDic.TryGetValue(eventNum, out d))
        {
            //执行委托之前，先判断委托是否为空
            if ((CallBack)d != null)
                ((CallBack)d)();
            else
            {
                //事件没有
                Debug.LogError("委托为空");
            }
        }
        else
        {
            //这个情况写的时候就会有语法报错
            Debug.Log("事件码不存在");
        }
    }
}
