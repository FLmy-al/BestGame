using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBackageButton : MonoBehaviour
{
    public Canvas backageCanvas;
    public void PutDown()
    {
        backageCanvas.gameObject.SetActive(true);
        BackageManager.instance.UpdateUI();
    }
}
