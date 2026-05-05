using UnityEngine;
using System.Collections.Generic;

public class BackageManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public int inventorySize = 20;
    public List<Item> items = new List<Item>();

    public static BackageManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 只在第一次创建时初始化
            items.Clear();
            for (int i = 0; i < inventorySize; i++)
            {
                items.Add(null);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(Item newItem)
    {
        if (newItem == null) return false;

        if (newItem.isStackable)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name != "" && items[i].name == newItem.name)
                {
                    items[i].count += newItem.count;
                    UpdateUI();
                    return true;
                }
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                items[i] = newItem;
                UpdateUI();
                return true;
            }
        }

        Debug.Log("背包满了");
        return false;
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name != "" && items[i].name == itemName)
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

    public bool FindItem(string itemName)
    {
        foreach (var item in items)
        {
            if (item != null && item.name == itemName)
                return true;
        }
        return false;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
                slots[i].SetItem(items[i]);
            else
                slots[i].SetItem(null);
        }
    }

    // 修复：加载存档时强制赋值 null
    public void LoadInventoryFromSave(List<Item> savedItems)
    {
        items.Clear();
        for (int i = 0; i < inventorySize; i++)
            items.Add(null);

        for (int i = 0; i < savedItems.Count && i < items.Count; i++)
        {
            var saveItem = savedItems[i];

            if (saveItem == null)
            {
                items[i] = null;
                continue;
            }

            Item originalItem = ItemManager.instance.GetItemById(saveItem.id);

            // 修复：找不到就设为空
            if (originalItem == null)
            {
                items[i] = null;
            }
            else
            {
                Item newItem = originalItem.Clone();
                newItem.count = saveItem.count;
                items[i] = newItem;
            }
        }

        UpdateUI();
    }
}