using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractive : MonoBehaviour
{
    private Collider2D collider_interactive; //碰撞体检测范围内可交互物体

    [SerializeField] private Image interactiveButton;
    [SerializeField] private InteractiveItem nearbyItem;

    private void Update()
    {
        if(nearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("与物品互动");
            BackageManager.instance.AddItem(nearbyItem.Item);
        }else
        {
            Debug.Log("无交互物体");
        }
    }

    //进入检测范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            Debug.Log("范围内有可交互物体");
            collision.gameObject.GetComponent<InteractiveItem>().Tip.gameObject.SetActive(true);
            nearbyItem = collision.gameObject.GetComponent<InteractiveItem>();
            //interactiveButton.gameObject.SetActive(true);
        }

        if (collision.tag == "NPC")
        {
            Debug.Log("范围内有可交互物体");
            //interactiveButton.gameObject.SetActive(true);
        }
    }
    //推出检测范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            Debug.Log("可交互物体超出范围");
            collision.gameObject.GetComponent<InteractiveItem>().Tip.gameObject.SetActive(false);
            nearbyItem = null;
            //interactiveButton.gameObject.SetActive(false);
        }
    }
}
