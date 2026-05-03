using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;           // 物品图标
    public TMP_Text countText;       // 数量文字

    private Item item;           // 当前格子里的物品

    // 设置格子物品
    public void SetItem(Item newItem)
    {
        item = newItem;
        if (item == null)
        {
            // 空格子
            icon.enabled = false;
            countText.text = "";
            return;
        }

        // 有物品
        icon.enabled = true;
        icon.sprite = item.icon;
        countText.text = item.count >= 1 ? item.count.ToString() : "";
    }
}
