using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseButton : MonoBehaviour
{
    public int nextId;  //对应下一段文本id
    public List<string> needItemName; //需要的物品名
    public GameObject colldier; //碰撞箱
    public void PutDown()
    {
        // 先判断是否需要物品
        bool hasRequiredItem = true;
        if (needItemName.Count != 0)
        {
            hasRequiredItem = CheckItems();
        }

        // 如果需要物品但没有，直接返回（不执行后续操作）
        if (!hasRequiredItem)
        {
            return;
        }else
        {
            if(colldier != null)
            {
                colldier.gameObject.SetActive(false);
            }
        }

        // 统一隐藏所有选项按钮
        foreach (var b in NarrativeManager.instance.choises)
        {
            if (b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(false);
            }
        }

        // 统一退出当前对话
        NarrativeManager.instance.ExitNarrative();

        // 如果有下一个剧情节点，就跳转
        if (nextId != 0)
        {
            NarrativeManager.instance.EnterNarrative(nextId);
        }
    }

    private bool CheckItems()
    {
        foreach(var b in needItemName)
        {
            if(!BackageManager.instance.FindItem(b))
            {
                Debug.Log("缺少所需物品：" + b);
                return false;
            }
        }
        return true;
    }
}
