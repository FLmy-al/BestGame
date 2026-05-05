using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;

    public Item item;

    public void SetItem(Item item, int count)
    {
        if (item == null || count <= 0)
        {
            icon.enabled = false;
            countText.text = "";
            return;
        }

        this.item = item;
        icon.enabled = true;
        icon.sprite = item.icon;
        countText.text = item.count.ToString();
    }
}