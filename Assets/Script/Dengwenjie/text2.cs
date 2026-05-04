using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text2 : MonoBehaviour
{
    public Vector2 minpostion;//最小移动
    public Vector2 maxpostion;//最大移动
    [SerializeField] private GameObject player; //获取玩家引用
    private void LateUpdate()
    {
        Vector3 targetPos=player.transform.position;
        targetPos.x=Mathf.Clamp(targetPos.x, minpostion.x, maxpostion.x);
        targetPos.y=Mathf.Clamp(targetPos.y, minpostion.y, maxpostion.y);
        transform.position = new Vector3(targetPos.x, targetPos.y + 3.0f, transform.position.z);
        
    }
    /// <summary>
    /// 获取地图信息，并赋值
    /// </summary>
    /// <param name="minpos"></param>
    /// <param name="maxpos"></param>
    public void SetCamPosLimit(Vector2 minpos,Vector2 maxpos)
    {
        minpostion = minpos;
        maxpostion = maxpos;
    }

}
