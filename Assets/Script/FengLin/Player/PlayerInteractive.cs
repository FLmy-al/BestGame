using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractive : MonoBehaviour
{
    private Collider2D collider_interactive; //碰撞体检测范围内可交互物体

    [SerializeField] private Image interactiveButton;

    //进入检测范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item" ||  collision.tag == "NPC")
        {
            Debug.Log("范围内有可交互物体");
            interactiveButton.gameObject.SetActive(true);
        }
    }
    //推出检测范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Item" || collision.tag == "NPC")
        {
            Debug.Log("可交互物体超出范围");
            interactiveButton.gameObject.SetActive(false);
        }
    }
}
