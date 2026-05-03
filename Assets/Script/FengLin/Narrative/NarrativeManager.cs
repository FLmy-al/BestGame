using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager instance;

    public Dictionary<int,bool> narrativeChoice = new Dictionary<int,bool>();
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
