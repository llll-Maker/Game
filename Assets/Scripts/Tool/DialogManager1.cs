using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager1 : MonoBehaviour
{
    public Text nameText;//姓名文本框
    public Text contentText;//对话文本框 
    public Image headImage;
    public Button button;
    private List<Message> messages;
    private int index = 0;//当前对话位置的索引
    private void Start()
    {
        //创建对话内容
        messages = new List<Message>() { new Message{Name="守护精灵",Content="恭喜你通过第一关的考验，在这一路你肯定收获颇丰吧！",HeadImage="sprite2" },
            new Message{Name=PlayerPrefs.GetString("name"),Content="是的，希望这一关的考验也能够顺利通过",HeadImage="people"},
            new Message{Name="守护精灵",Content="会的，这一关只是环境稍微阴森了点",HeadImage="sprite2"},
            new Message{Name=PlayerPrefs.GetString("name"),Content="哈哈哈哈，希望如此吧",HeadImage="people"},
            new Message{Name="守护精灵",Content="加油！通过点击任务按钮可以知道这一关的任务", HeadImage = "sprite2"},
            new Message{Name=PlayerPrefs.GetString("name"),Content="好的",HeadImage = "people"},
            new Message{Name="守护精灵",Content="温馨提示，请勿离僵尸太近，不然会被开枪的", HeadImage = "sprite2"},
            new Message{Name="守护精灵",Content="对了，有些地方是禁区，进去了就出不来了，请小心周围环境", HeadImage = "sprite2"},
        };
    }
    private void Update()
    {
        UpdateMessage();
    }
    private void UpdateMessage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Message msg = GetMessage();
            if (msg != null)
            {
                nameText.text = msg.Name;
                contentText.text = msg.Content;
                headImage.sprite = GameFacade.Instance.LoadSprite(msg.HeadImage);
            }
            else
            {
                GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(false);
            }
        }
    }
    private Message GetMessage()
    {
        if(index<messages.Count)
        {
            return messages[index++];
        }
        return null;
    }
}
public class Message
{
    public string Name { get; set; }
    public string Content { get; set; }
    public string HeadImage { get; set; }
}

