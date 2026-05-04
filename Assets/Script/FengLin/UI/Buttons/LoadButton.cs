using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private int slot;
    public void PutDown()
    {
        SaveData saveData = SaveManager.instance.Load(slot);

        PlayerMovement.instance.transform.position = saveData.playerPos;
        // 恢复剧情选择（把 List 转回 Dictionary）
        NarrativeManager.instance.narrativeChoice.Clear();
        NarrativeManager.instance.narrativeChoice = saveData.playerChoices;
    }
}
