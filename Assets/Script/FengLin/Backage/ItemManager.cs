using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    // 必须在 Inspector 里拖入所有物品
    public List<Item> allItems;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 正确写法：从总物品库里找，不是从背包里找
    public Item GetItemById(int id)
    {
        foreach (var item in allItems)
        {
            if (item != null && item.id == id)
                return item;
        }
        return null;
    }
}