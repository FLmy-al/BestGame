using UnityEngine;
using System.Collections.Generic;

public class BackageManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public int inventorySize = 20;

    // 换成 Slot 列表，不再直接存 Item
    public List<ItemSlot> slotsList = new List<ItemSlot>();

    public static BackageManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // 初始化：全部是“空槽位”
        slotsList.Clear();
        for (int i = 0; i < inventorySize; i++)
        {
            slotsList.Add(new ItemSlot()); // 里面 item=null
        }
    }

    public bool AddItem(Item newItem, int addCount = 1)
    {
        if (newItem == null) return false;

        // 1. 可堆叠 → 找同物品叠加
        if (newItem.isStackable)
        {
            foreach (var slot in slotsList)
            {
                if (!slot.IsEmpty && slot.item.id == newItem.id)
                {
                    slot.count += addCount;
                    UpdateUI();
                    return true;
                }
            }
        }

        // 2. 找真正的空格子（IsEmpty）
        foreach (var slot in slotsList)
        {
            if (slot.IsEmpty)
            {
                slot.item = newItem;
                slot.count = addCount;
                UpdateUI();
                return true;
            }
        }

        Debug.Log("背包已满！");
        return false;
    }

    public void RemoveItem(string itemName)
    {
        foreach (var slot in slotsList)
        {
            if (!slot.IsEmpty && slot.item.name == itemName)
            {
                slot.count--;
                if (slot.count <= 0)
                    slot.Clear();

                UpdateUI();
                return;
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= slotsList.Count)
            {
                slots[i].SetItem(null, 0);
                continue;
            }

            var slot = slotsList[i];
            slots[i].SetItem(slot.item, slot.count);
        }
    }

    public bool FindItem(string ItemName)
    {
        foreach (var slot in slotsList)
        {
            if((slot.item != null) && (slot.item.name == ItemName))
            {
                return true;
            }
        }

        return false;
    }
}