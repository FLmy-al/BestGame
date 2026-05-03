using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private int slot;
    public void PutDown()
    {
        SaveManager.instance.Save(slot);
    }
}
