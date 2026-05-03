using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Vector3 playerPos; //玩家位置
    public int level; //关卡
    public List<ChoiceData> playerChoices = new List<ChoiceData>(); //剧情节点

    //无参构造
    public SaveData() { }
    /// <summary>
    /// 带参构造 玩家位置，关卡编号，分支选择
    /// </summary>
    /// <param name="playerTransform"></param>
    /// <param name="level"></param>
    /// <param name="playerChoice"></param>
    public SaveData(Vector3 playerPos, int level, Dictionary<int, bool> playerChoice)
    {
        this.playerPos = playerPos;
        this.level = level;
        // 把 Dictionary 转成 List
        foreach (var kvp in playerChoice)
        {
            playerChoices.Add(new ChoiceData()
            {
                id = kvp.Key,
                selected = kvp.Value
            });
        }
    }
}

public class ChoiceData
{
    public int id;
    public bool selected;
}
