using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBackageButton : MonoBehaviour
{
    public Canvas backageCanvas;
    public void PutDown()
    {
        backageCanvas.gameObject.SetActive(false);
    }
}
