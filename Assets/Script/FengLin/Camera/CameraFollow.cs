using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player; //获取玩家引用
    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, transform.position.z);
    }
}
