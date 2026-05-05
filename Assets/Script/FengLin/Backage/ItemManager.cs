using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    //public List<Item> allItems; // 把你所有物品拖进去

    private void Awake()
    {
        instance = this;
    }

    public Item GetItemById(int id)
    {
        foreach (var item in BackageManager.instance.items)
        {
            if (item.id == id)
                return item;
        }
        return null;
    }
}