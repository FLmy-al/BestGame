using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnWall : MonoBehaviour,ISaveInterface
{
    public Vector3 returnPos = new Vector3(-47, 0, 0);
    public int narrativeNodeid;

    public string UniqueId; //³¡¾°±êÊ¶id

    public string GetUniqueId() => UniqueId;

    public void SetNarrativeNode()
    {
        foreach (var node in NarrativeManager.instance.narrativeChoice)
        {
            if (node.Id == narrativeNodeid)
            {
                node.finished = false;
            }
        }
    }

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
