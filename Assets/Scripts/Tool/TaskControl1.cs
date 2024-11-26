using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskControl1 : MonoBehaviour
{
    //private GameObject Player;
    private GameObject UIRoot;
    private GameObject Task;
    private List<string> tasks;
    private Text taskName;
    public Sprite image;
    private bool isComplete = false;
    private bool isComplete1 = false;
    public Text nameText;//姓名文本框
    public Text contentText;//对话文本框 
    public Image headImage;
    public Button button;
    private List<Message> messages;
    private int index;
    private bool isclick = false;
    private bool isclick1 = false;
    private bool isclick2 = false;
    public item GetItem;
    public item GetItem1;
    public item GetItem2;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        UIRoot = GameObject.Find("Canvas");
        Task = UIRoot.transform.GetChild(5).gameObject;
        messages = new List<Message>() {
            new Message{Name="小狐狸",Content="滴滴滴滴,成功触发剧情",HeadImage="fox" },
            new Message{Name="小狐狸",Content="恭喜你成功通过第一个任务,你在世界所得到的这些物品可以给你涨经验值哦",HeadImage="fox" },
            new Message{Name="小狐狸",Content="接下来，你就要继续开启新的挑战了，好运哦",HeadImage="fox"},
            new Message{Name=PlayerPrefs.GetString("name"),Content="好的，我一定会加油的",HeadImage="people"},
        };
        tasks = new List<string>() {
            Task.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text,
            Task.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>().text,
            Task.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text};
        taskName = Task.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        Task.GetComponent<Button>().onClick.AddListener(() =>
        { //先禁用其他两个任务的按钮
            Task.transform.GetChild(0).gameObject.SetActive(true);
            Task.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Button>().interactable = false;
            Task.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Button>().interactable = false;
        });
        Task.transform.GetChild(0).GetChild(5).GetComponent<Button>().onClick.AddListener(() =>
        {
            //关闭任务的按钮
            Task.transform.GetChild(0).gameObject.SetActive(false);
        });
        Task.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            //接受任务一，任务一变成进行中，然后显示任务名称，
            Task.transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(false);
            Task.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(true);
            Task.transform.GetChild(1).gameObject.SetActive(true);
            StartNextTask(0);
            isclick = true;
        });
        Task.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            //任务二
            Task.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
            Task.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);
            Task.transform.GetChild(1).gameObject.SetActive(true);
            isclick1 = true;

        });
        Task.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            //任务三
            Task.transform.GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(false);
            Task.transform.GetChild(0).GetChild(4).GetChild(2).gameObject.SetActive(true);
            Task.transform.GetChild(1).gameObject.SetActive(true);
            isclick2 = true;
        });
        EventCenter.AddListerner(EventNum.Win, CompleteTask3);
        EventCenter.AddListerner(EventNum.Defeat, FailTask3);
    }

    // Update is called once per frame
    void Update()
    {
        if (isclick)
        {
            CompleteTask1();
        }
        if (isComplete)
        {
            CancelInvoke("CompleteTask1");
            Task.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Button>().interactable = true;
            if (isclick1 && isclick)
            {
                CompleteTask2();
            }
        }
        if (isComplete1)
        {
            Debug.Log(isComplete1);
            Task.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
            Task.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(false);
            Task.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
            Task.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Button>().interactable = true;
            taskName.text = "当前任务已完成";
            if (isclick2)
                StartNextTask(2);
        }
        //EventCenter.AddListerner(EventNum.Win1, CompleteTask3);
        //EventCenter.AddListerner(EventNum.Defeat1, FailTask3);
        //EventCenter.AddListerner(EventNum.Win2, CompleteTask3);
        //EventCenter.AddListerner(EventNum.Defeat2, FailTask3);

    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(EventNum.Defeat, FailTask3);
        EventCenter.RemoveListener(EventNum.Win, CompleteTask3);
    }
    /// <summary>
    /// 开启下一个任务
    /// </summary>
    private void StartNextTask(int currentIndex)
    {
        if (currentIndex < tasks.Count)
        {
            string currentTask = tasks[currentIndex];
            taskName.text = "当前任务：" + currentTask.ToString();
            //等到上一个任务完成时才开启下一个任务
        }
        else
        {
            taskName.text = "所有任务都已完成";
            //todo:领取奖励
        }
    }
    private void CompleteTask1()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            //Debug.Log(inventoryManager.instance.slots[i].GetComponent<Slot>().slotImage.sprite.name);
            if (inventory.items[i] == null) continue;
            if (image.name == inventory.items[i].itemImage.name)
            {

                if (inventory.items[i].itemHeld>=3)
                {
                    //Debug.Log(inventoryManager.instance.myBag.items[i].itemHeld);
                    //完成一个任务
                    Task.transform.GetChild(0).GetChild(2).GetChild(1).gameObject.SetActive(false);
                    Task.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                    Task.transform.GetChild(0).GetChild(2).GetChild(3).gameObject.SetActive(true);
                    Task.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Button>().interactable = true;
                    taskName.text = "当前任务已完成";
                    isComplete = true;
                    break;
                }
                break;
                //return;
            }

        }

    }

    private void CompleteTask2()
    {
        StartNextTask(1);
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Fox").transform.position) < 1.5f)
        {
            GameObject.FindGameObjectWithTag("Fox").transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject.Find("Canvas").transform.GetChild(14).gameObject.SetActive(true);
                Message msg = GetMessage();
                if (msg != null)
                {
                    nameText.text = msg.Name;
                    contentText.text = msg.Content;
                    headImage.sprite = GameFacade.Instance.LoadSprite(msg.HeadImage);
                }
                else
                {
                    GameObject.Find("Canvas").transform.GetChild(14).gameObject.SetActive(false);
                    isComplete1 = true;

                }
            }

        }
    }
    private Message GetMessage()
    {
        if (index < messages.Count)
        {
            return messages[index++];
        }
        return null;
    }

    private void CompleteTask3()
    {
        //分为完成和不完成
        //不完成则显示任务失败，完成则显示任务成功
        //taskName.text = "当前任务成功";
        Task.transform.GetChild(1).gameObject.SetActive(false);
        Task.transform.GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(false);
        Task.transform.GetChild(0).GetChild(4).GetChild(2).gameObject.SetActive(false);
        Task.transform.GetChild(0).GetChild(4).GetChild(3).gameObject.SetActive(true);
        //更改数据的值
        //先更改经验，升三级，奖励1200
        //更改背包 显示技能药水
        if (!inventory.items.Contains(GetItem))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i] == null)
                {
                    inventory.items[i] = GetItem;
                    break;
                }

            }
        }
        else
            GetItem.itemHeld++;
        if (!inventory.items.Contains(GetItem2))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i] == null)
                {
                    inventory.items[i] = GetItem2;
                    GetItem2.itemHeld = 1;
                    string str3 = PlayerPrefs.GetString("bag");
                    string str4 = str3 + ",三叶草";
                    MySQL sql1 = new MySQL();
                    sql1.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str4 }, "name", PlayerPrefs.GetString("name"));
                    PlayerPrefs.SetString("bag", str4);
                    sql1.Close();
                    break;
                }

            }
            
        }
        else
            GetItem2.itemHeld++; ;//三叶草
        inventoryManager.RefreshItem();
        int level = PlayerPrefs.GetInt("gamelevel");
        int money = PlayerPrefs.GetInt("money");
        string str = PlayerPrefs.GetString("bag");
        string str1 = str + ",技能药水";
        money += 1200;
        level += 3;
        MySQL sql = new MySQL();
        sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "gamelevel", "money", "bag" }, new string[] { level.ToString(), money.ToString(), str1 }, "name", PlayerPrefs.GetString("name"));
        //更新一下
        PlayerPrefs.SetInt("gamelevel", level);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetString("bag", str1);
        sql.Close();
        UIRoot.transform.GetChild(11).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = level.ToString();

    }
    private void FailTask3()
    {
        //任务失败
        //taskName.text = "当前任务失败";
        Task.transform.GetChild(1).gameObject.SetActive(false);
        Task.transform.GetChild(0).GetChild(4).GetChild(2).gameObject.SetActive(false);
        Task.transform.GetChild(0).GetChild(4).GetChild(4).gameObject.SetActive(true);
        //更改背包，先是伤害药水
        if (!inventory.items.Contains(GetItem1))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i] == null)
                {
                    inventory.items[i] = GetItem1;
                    break;
                }

            }
           
        }
        else
            GetItem1.itemHeld++;

        if (!inventory.items.Contains(GetItem2))
        {
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i] == null)
                {
                    inventory.items[i] = GetItem2;
                    GetItem2.itemHeld = 1;
                    string str3 = PlayerPrefs.GetString("bag");
                    string str4 = str3 + ",三叶草";
                    MySQL sql1 = new MySQL();
                    sql1.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str4 }, "name", PlayerPrefs.GetString("name"));
                    PlayerPrefs.SetString("bag", str4);
                    sql1.Close();
                    break;
                }

            }
        }
        else
            GetItem2.itemHeld++;;//三叶草
        inventoryManager.RefreshItem();
        //更改数据的值(升一级，奖励400）
        int level = PlayerPrefs.GetInt("gamelevel");
        level += 1;
        int money = PlayerPrefs.GetInt("money");
        string str = PlayerPrefs.GetString("bag");
        string str1 = str + ",伤害药水";
        money += 400;
        MySQL sql = new MySQL();
        sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "gamelevel", "money", "bag" }, new string[] { level.ToString(), money.ToString(), str1 }, "name", PlayerPrefs.GetString("name"));
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetString("bag", str1);
        PlayerPrefs.SetInt("gamelevel", level);
        sql.Close();
        UIRoot.transform.GetChild(12).GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>().text = level.ToString();
    }


}
//[Serializable]
//public class Task
//{
//    public string name;
//}
