using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    public void PutDown()
    {
        SaveData saveData = new SaveData(PlayerMovement.instance.transform,0,NarrativeManager.instance.narrativeChoice);
        SaveManager.Save(saveData);
    }
}
