using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InteractiveItem : MonoBehaviour
{
    [SerializeField]private Item item;
    public GameObject Tip; // 接近提示
    public Item Item => item; //可获得物品
}
