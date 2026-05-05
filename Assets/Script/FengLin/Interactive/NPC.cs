using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour,ISaveInterface
{
    public int narrativeId;
    public GameObject Tip; // 接近提示

    public string UniqueId;

    public string GetUniqueId() => UniqueId;

    public ObjectState SaveState()
    {
        return new ObjectState
        {
            isActive = gameObject.activeSelf,
            position = transform.position,
            rotation = transform.rotation
        };
    }

    public void LoadState(ObjectState state)
    {
        gameObject.SetActive(state.isActive);
        transform.position = state.position;
        transform.rotation = state.rotation;
    }
}
