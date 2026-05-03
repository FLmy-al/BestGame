using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackageManager : MonoBehaviour
{
    public InventorySlot[] slots;  // 所有背包格子
    public int inventorySize = 20; // 背包总格子数
    private List<Item> items = new List<Item>(); //物品列表

    public static BackageManager instance; //单例模式

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

        // 初始化物品列表
        for (int i = 0; i < inventorySize; i++)
        {
            items.Add(null);
        }
    }

    // 添加物品到背包
    public bool AddItem(Item newItem)
    {
        Debug.Log("添加物品");
        // 1. 如果物品可堆叠，先找已有的格子
        if (newItem.isStackable)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null && items[i].name == newItem.name)
                {
                    items[i].count += newItem.count;
                    UpdateUI();
                    return true;
                }
            }
        }

        // 2. 找空格子放新物品
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                UpdateUI();
                return true;
            }
        }

        // 背包满了
        Debug.Log("背包已满！");
        return false;
    }

    // 移除物品
    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null && items[i].name == itemName)
            {
                items[i].count--;
                if (items[i].count <= 0)
                {
                    items[i] = null;
                }
                UpdateUI();
                return;
            }
        }
    }

    // 更新所有格子的UI显示
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }
}
