using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public int id; //物品id
    public string name; //物品名称
    public string description; //物品描述
    public Sprite icon; //物品图标
    public int count; //物品堆叠数量
    public bool isStackable; //是否可堆叠
}
