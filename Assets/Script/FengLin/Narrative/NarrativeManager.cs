using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager instance;

    public List<NarrativeNode> narrativeChoice = new List<NarrativeNode>(); //剧情节点列表
    public List<Button> choises = new List<Button>();  //分支选项按钮
    public Canvas UIcanvas;            //UI画布
    public Canvas NarrativeCanvas;     //对话画布
    public TMP_Text SpeakerName;       //说话人姓名
    public TMP_Text dialogueText;      //剧情文本
    public Image SpeakerImage;         //说话人立绘
    public Speaker currentSpeaker;     //当前说话人物

    public bool OnNarrative;           //是否打开对话界面
    public bool OnChoise = false;              //是否需要选择选项
    public NarrativeNode currentNode;  //当前剧情节点
    public int ContentIndex = 0; //当前对话内容下标

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //处于对话界面时，按鼠标左键更新文本
        if(OnNarrative && !OnChoise && Input.GetMouseButtonDown(0))
        {
            if(ContentIndex >= currentNode.narrativeContents.Count)
            {
                ExitNarrative();
            }else
            {
                UpdateDialogue(currentNode.narrativeContents[ContentIndex]);
            }
        }
    }
    //进入对话
    public void EnterNarrative(int id)
    {
        currentNode = GetNarrativeNode(id);
        if(currentNode == null)
        {
            return;
        }
        ContentIndex = 0;
        UIcanvas.gameObject.SetActive(false);
        NarrativeCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        OnNarrative = true;
        OnChoise = false;
        //currentSpeaker = currentNode.narrativeContents[ContentIndex].speaker;
        UpdateDialogue(currentNode.narrativeContents[ContentIndex]);
    }
    //退出对话
    public void ExitNarrative()
    {
        UIcanvas.gameObject.SetActive(true);
        NarrativeCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        OnNarrative = false;
        currentNode.Finish();
        currentNode = null;
    }
    //通过id寻找剧情节点
    public NarrativeNode GetNarrativeNode(int id)
    {
        foreach(var node in narrativeChoice)
        {
            if(node.Id == id && !node.finished)
            {
                return node;
            }
        }
        Debug.Log("未找到剧情节点或剧情已完成");
        return null;
    }
    //更新对话显示
    public void UpdateDialogue(NarrativeContent narrativeContent)
    {
        if(currentSpeaker != narrativeContent.speaker)
        {
            currentSpeaker = narrativeContent.speaker;
            SpeakerName.text = narrativeContent.speaker.name;
            SpeakerImage.sprite = narrativeContent.speaker.sprite;
        }

        if(narrativeContent.choises.Count != 0)
        {
            for(int i = 0; i < narrativeContent.choises.Count;  i++)
            {
                choises[i].gameObject.SetActive(true);
                choises[i].GetComponent<ChoiseButton>().nextId = narrativeContent.choises[i].nextId;
                choises[i].GetComponent<ChoiseButton>().needItemName = narrativeContent.choises[i].needItemName;
                choises[i].GetComponent<ChoiseButton>().GiveItemName = narrativeContent.choises[i].GiveItemName;
                choises[i].GetComponentInChildren<TMP_Text>().text = narrativeContent.choises[i].text;
                if(narrativeContent.choises[i].colldier != null)
                {
                    choises[i].GetComponent<ChoiseButton>().colldier = narrativeContent.choises[i].colldier;
                }
            }
            OnChoise = true;
        }
        Debug.Log(OnChoise);
        
        dialogueText.text = narrativeContent.Content;
        ContentIndex++;
        Debug.Log(ContentIndex);
    }
}
