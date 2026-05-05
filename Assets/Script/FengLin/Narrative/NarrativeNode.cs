using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
//对话节点
public class NarrativeNode
{
    public int Id;            //对话节点id
    public List<NarrativeContent> narrativeContents = new List<NarrativeContent>(); //对话内容列表
    public bool finished = false;     //已完成标记

    public void Finish()
    {
         finished = true;
    }
}
//对话内容
[Serializable]
public class NarrativeContent
{
    public string Content;  //对话文本
    public Speaker speaker;  //说话人
    public List<Choise> choises = new List<Choise>(); //分支选项列表
}
[Serializable]
public class Speaker
{
    public string name;      //姓名
    public Sprite sprite;    //立绘
}
[Serializable]
public class Choise
{
    public int nextId;    //连接的对话节点id
    public string text;   //选项文本
    public string needItemName; //需要的物品名
    public GameObject colldier; //碰撞箱
}
