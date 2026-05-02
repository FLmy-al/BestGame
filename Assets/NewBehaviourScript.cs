using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    /// <summary>
    /// 对话文本
    /// </summary>
    public TextAsset duihua;

    /// <summary>
    /// 左
    /// </summary>
    public SpriteRenderer left;

    /// <summary>
    /// 右
    /// </summary>
    public SpriteRenderer right;

    /// <summary>
    /// 名字文本
    /// </summary>
    public TMP_Text nameText;

    /// <summary>
    /// 对话内容文本
    /// </summary>
    public TMP_Text duihuaneirong;

    /// <summary>
    /// 角色图片列表
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// 角色名字对应图片的字典
    /// </summary>
    Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();

    /// <summary>
    /// 保持当前对话索引值
    /// </summary>
    public int neirongindex;

    /// <summary>
    /// 对话文本分割
    /// </summary>
    public string[] rows;

    /// <summary>
    /// 对话继续按钮
    /// </summary>
    public Button nextbutton;

    /// <summary>
    /// 选项预制体
    /// </summary>
    public GameObject optionbutton;

    /// <summary>
    /// 选项按钮父节点 用于排序--自动排列
    /// </summary>
    public Transform buttonGroup;
    private void Awake()
    {
        images["甲"] = sprites[0];
        images["乙"] = sprites[1];
    }
    // Start is called before the first frame update
    void Start()
    {
        ReadText(duihua);
        Showneirong();
        //UpdateText("甲", "你好，世界");
        //UpdateImage("乙", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string _name,string _text)
    {
        nameText.text = _name;
        duihuaneirong.text = _text;
    }
    public void UpdateImage(string _name, string _position)
    {
        if (_position=="左")
        {
            left.sprite = images[_name];
        }
        else if(_position=="右")
        {
            right.sprite = images[_name];
        }
    }
    public void ReadText(TextAsset _textAsset)
    {
        rows = _textAsset.text.Split('\n');
        //foreach(var row in rows)
        //{
        //    string[] cell = row.Split(',');
        //}
        Debug.Log("读取成功");
    }
    public void Showneirong()
    {
        for(int i=0;i<rows.Length;i++)
        {
            string[] cells = rows[i].Split(',');
            if ( cells[0]=="#" && int.Parse(cells[1]) == neirongindex)
            {
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);

                neirongindex = int.Parse (cells[5]);
                nextbutton.gameObject.SetActive(true);
                break;
            }
            else if (cells[0]=="&" && int.Parse(cells[1]) == neirongindex)
            {
                nextbutton .gameObject.SetActive(false);//隐藏原来的按钮
                GenerateOption(i);
            }
            else if(cells[0] == "END" && int.Parse(cells[1]) == neirongindex)
            {
                Debug.Log("剧情结束");
            }
        }
    }
    public void OnclickNext()
    {
        Showneirong();
    }

    public void GenerateOption(int _index)
    {
        string[] cells = rows[_index].Split(',');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionbutton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(delegate { OnOptionClick(int.Parse(cells[5])); });
            GenerateOption(_index + 1);
        }

        
    }
    public void OnOptionClick(int _id)
    {
        neirongindex = _id;
        Showneirong();
        for(int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }
}
