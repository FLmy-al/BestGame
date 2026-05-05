using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public int level;
    public List<NarrativeNode> playerChoices = new List<NarrativeNode>();
    public List<Item> inventoryItems = new List<Item>();

    // 改用 List 才能被 Json 正常保存
    public List<ObjectState> allObjStates = new List<ObjectState>();

    public SaveData() { }

    public SaveData(Vector3 playerPos, int level, List<NarrativeNode> playerChoice)
    {
        this.playerPos = playerPos;
        this.level = level;
        this.playerChoices = playerChoice;
    }
}