using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [Header("UI引用")]
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescText;


    [SerializeField]private Item currentHoverItem;
    private void Update()
    {
        // 鼠标位置转世界坐标，发射射线
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity);

        //检测到物品
        if (hit.collider != null && hit.collider.CompareTag("BackageGrid") && hit.collider.GetComponent<InventorySlot>(). != null)
        {
            Debug.Log("检测到物品" + hit.collider.GetComponent<InventorySlot>().item.name);
            // 显示面板
            detailPanel.gameObject.SetActive(true);
            Item item = hit.collider.GetComponent<InventorySlot>().item;
            if (item != null)
            {
                // 避免重复刷新，性能优化
                if (currentHoverItem != item)
                {
                    currentHoverItem = item;
                    // 更新面板内容
                    itemNameText.text = item.name;
                    itemDescText.text = item.description;
                    
                }
            }
        }
        //没检测到物品，隐藏面板
        else
        {
            if(currentHoverItem != null)
            {
                currentHoverItem = null;
                detailPanel.SetActive(false);
            }
        }
    }
}
