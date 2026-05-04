using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Vector3 playerPos; //玩家位置
    public int level; //关卡
    public List<NarrativeNode> playerChoices = new List<NarrativeNode>(); //剧情节点

    //无参构造
    public SaveData() { }
    /// <summary>
    /// 带参构造 玩家位置，关卡编号，分支选择
    /// </summary>
    /// <param name="playerTransform"></param>
    /// <param name="level"></param>
    /// <param name="playerChoice"></param>
    public SaveData(Vector3 playerPos, int level, List<NarrativeNode> playerChoice)
    {
        //保存玩家位置
        this.playerPos = playerPos;
        //保存关卡
        this.level = level;
        //保存剧情进度
        this.playerChoices = playerChoice;
    }
}