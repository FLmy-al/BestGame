using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private int slot;

    public void PutDown()
    {
        SaveManager.instance.Load(slot);
    }
}