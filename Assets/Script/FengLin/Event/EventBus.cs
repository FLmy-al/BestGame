using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public static class EventBus
//{
//    private static Dictionary<string, Delegate> eventDic = new Dictionary<string, Delegate>(); //事件字典

//    //订阅事件
//    public static void Subscribe(string eventName, Action action)
//    {
//        if(!eventDic.ContainsKey(eventName))
//        {
//            eventDic.Add(eventName, action);
//        }else
//        {
//            eventDic[eventName] = Delegate.Combine(eventDic[eventName],action);
//        }
//    }

//    //发布事件
//    public static void Publich(string eventName)
//    {
//        if(eventDic.TryGetValue(eventName, out Delegate del))
//        {
//            if(del is Action act)
//            {
//                act?.Invoke();
//            }
//        }
//    }
//    //取消订阅
//    public static void Unsubscribe(string eventName, Action action)
//    {
//        if (eventDic.TryGetValue(eventName, out Delegate del))
//        {
//            Delegate newDel = Delegate.Remove(del, action);
//            if (newDel == null)
//                eventDic.Remove(eventName);
//            else
//                eventDic[eventName] = newDel;
//        }
//    }
//}
