using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteractive : MonoBehaviour
{
    private Collider2D collider_interactive; //碰撞体检测范围内可交互物体

    [SerializeField] private Image interactiveButton;
    [SerializeField] private InteractiveItem nearbyItem;
    [SerializeField] private NPC nearbyNpc;

    private void Update()
    {
        if(nearbyItem != null && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("与物品互动");
            Item newitem = nearbyItem.Item.Clone();
            BackageManager.instance.AddItem(newitem);
            nearbyItem.gameObject.SetActive(false);
            nearbyItem = null;
        }else if(nearbyNpc != null && Input.GetKeyUp(KeyCode.E))
        {
            NarrativeManager.instance.EnterNarrative(nearbyNpc.narrativeId);
        }
    }

    //进入检测范围
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            Debug.Log("范围内有可交互物体");
            if(collision.gameObject.GetComponent<InteractiveItem>().Tip != null)
            {
                collision.gameObject.GetComponent<InteractiveItem>().Tip.gameObject.SetActive(true);
            }
            nearbyItem = collision.gameObject.GetComponent<InteractiveItem>();
        }

        if (collision.tag == "NPC")
        {
            Debug.Log("范围内有可交互物体");
            if(collision.gameObject.GetComponent<NPC>().Tip != null)
            {
                collision.gameObject.GetComponent<NPC>().Tip.gameObject.SetActive(true);
                nearbyNpc = collision.gameObject.GetComponent<NPC>();
            }
        }

        if(collision.tag == "collider")
        {
            PlayerMovement.instance.transform.position = collision.gameObject.GetComponent<ReturnWall>().returnPos;
            collision.gameObject.GetComponent<ReturnWall>().SetNarrativeNode();
        }

        if(collision.tag == "InteractiveCollider")
        {
            NarrativeManager.instance.EnterNarrative(collision.gameObject.GetComponent<NPC>().narrativeId);
        }

        if(collision.tag == "EnterNextScene")
        {
            SceneManager.LoadScene(1);
            BackageManager.instance.UpdateUI();
        }
    }
    //退出检测范围
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            Debug.Log("可交互物体超出范围");
            if (collision.gameObject.GetComponent<InteractiveItem>().Tip != null)
            {
                collision.gameObject.GetComponent<InteractiveItem>().Tip.gameObject.SetActive(false);
            }
            nearbyItem = null;
        }

        if (collision.tag == "NPC")
        {
            Debug.Log("可交互物体超出范围");
            if (collision.gameObject.GetComponent<NPC>().Tip != null)
            {
                collision.gameObject.GetComponent<NPC>().Tip.gameObject.SetActive(false);
                nearbyNpc = null;
            }
        }
    }
}
