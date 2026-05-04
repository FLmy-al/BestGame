using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveInterface
{
    string GetUniqueId(); // 必须加这个
    ObjectState SaveState();
    void LoadState(ObjectState state);
}

[System.Serializable]
public class ObjectState
{
    public string id;          // 物体唯一ID
    public bool isActive;
    public Vector3 position;
    public Quaternion rotation;
}
