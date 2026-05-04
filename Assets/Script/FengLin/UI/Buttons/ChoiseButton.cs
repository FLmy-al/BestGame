using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseButton : MonoBehaviour
{
    public int nextId;  //对应下一段文本id
    public void PutDown()
    {
        //设置当前剧情节点
        NarrativeManager.instance.ExitNarrative();
        NarrativeManager.instance.EnterNarrative(nextId);
        
        //隐藏按钮
        foreach (var b in NarrativeManager.instance.choises)
        {
            if(b.gameObject.activeSelf)
            {
                b.gameObject.SetActive(false);
            }
        }
    }
}
