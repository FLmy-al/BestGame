using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Transform playerTransform; //玩家位置
    public int level; //关卡
    public Dictionary<int, bool> playerChoice = new Dictionary<int, bool>();

    //无参构造
    public SaveData() { }
    /// <summary>
    /// 带参构造 玩家位置，关卡编号，分支选择
    /// </summary>
    /// <param name="playerTransform"></param>
    /// <param name="level"></param>
    /// <param name="playerChoice"></param>
    public SaveData(Transform playerTransform, int level, Dictionary<int, bool> playerChoice)
    {
        this.playerTransform = playerTransform;
        this.level = level;
        this.playerChoice = playerChoice;
    }
}
