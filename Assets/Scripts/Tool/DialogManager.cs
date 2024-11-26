using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
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
        messages = new List<Message>() { new Message{Name="守护精灵",Content="欢迎来到我们这里做客,我们这里资源丰富，是一个休息的好地方呢！",HeadImage="sprite1" },
            new Message{Name=PlayerPrefs.GetString("name"),Content="是的，我也是慕名而来的，但是在路上也听说了你们这里突然遭遇了一些不太好的事情",HeadImage="people"},
            new Message{Name="守护精灵",Content="是的，我们也在寻找着勇士来帮助我们度过这次难关",HeadImage="sprite1"},
            new Message{Name=PlayerPrefs.GetString("name"),Content="我也可以帮上忙的!",HeadImage="people"},
            new Message{Name="守护精灵",Content="真是太感谢你了，我们有下发一些任务，你可以试着去完成,完成所有任务后会有丰富的奖励哦", HeadImage = "sprite1"},
            //new Message{Name="守护精灵",Content="但是这次的僵尸有个奇怪的特性，他们同生共死，击败其中一个，任务即可完成", HeadImage = "sprite1"},
            new Message{Name=PlayerPrefs.GetString("name"),Content="我一定会尽力完成的",HeadImage = "people"},
            new Message{Name="守护精灵",Content="温馨提示,场景里的物体只有固定的才能被找到哦", HeadImage = "sprite1"},
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

