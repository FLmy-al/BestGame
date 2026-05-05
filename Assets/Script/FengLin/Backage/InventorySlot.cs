using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;           // 物品图标
    public TMP_Text countText;       // 数量文字

    public Item item;           // 当前格子里的物品

    private void Start()
    {
        item = null;
    }
    // 设置格子物品
    public void SetItem(Item newItem)
    {
        // 名字为空直接当空格子
        if (newItem == null || string.IsNullOrEmpty(newItem.name))
        {
            icon.enabled = false;
            countText.text = "";
            item = null;
            return;
        }

        item = newItem;
        icon.enabled = true;
        icon.sprite = item.icon;
        countText.text = item.count >= 1 ? item.count.ToString() : "";
    }
}
