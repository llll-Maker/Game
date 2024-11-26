using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LogicRegisterManager : MonoBehaviour
{
    [Header("登录")]
    public GameObject LogicPanel;
    public InputField L_If_Name;
    public InputField L_If_Password;
    public Button Btn_Logic;
    public Button Btn_ToRegister;

    [Header("注册")]
    public GameObject RegisterPanel;
    public InputField R_If_Name;
    public InputField R_If_Password;
    public InputField R_Password;
    public Dropdown D_Sex;
    public Button Btn_Register;
    public Button Btn_Back;
    
    [Header("游戏")]
    public GameObject gamePanel;
    public GameObject Card;
    public Text Txt_username;
    public Text Txt_gamelevel;
    public Text Txt_money;
    public Button B_set;
    public Button B_GameStart;
    public Button B_Bag;
    //public Button B_Deck;
    public Button B_Quest;
    public Button B_Back;

    [Header("个人中心")]
    public GameObject SCPanel;
    public GameObject CancelPanel;
    public GameObject UpdatePanel;
    public Text Txt_SC_Name;
    public Text Txt_SC_GameLevel;
    public Text Txt_SC_Money;
    public InputField SC_If_Password;
    public InputField SC_If_Password1;
    public Dropdown SC_Dp_Sex;
    public Button Btn_SC_Update;
    public Button Btn_SC_Delete;
    public Button Btn_SC_Back;
    public Button Btn_Can_Yes;
    public Button Btn_Can_No;
    public Button Btn_Up_Yes;
    public Button Btn_Up_No;

    [Header("背包")]
    public GameObject BagPanel;
    public Button Bag_Back;
    public Text text_Remain;
    public Button Btn_explanation;
    private bool isOpen = false;
    //private bool isOpen1 = false;
    private void Awake()
    {
        Btn_Logic.onClick.AddListener(() =>
        {
            if(L_If_Name.text == "" || L_If_Password.text == "")
            {
                ShowRemainText("账户和密码不能为空!!!");
                return;
            }

            MySQL sql=new MySQL();
            bool isHas = false;
            MySqlDataReader read1 = sql.Select("20213908_衷茜芝_gamedate");//调用Select函数
            while (read1.Read())
            {
                string name = read1.GetString("name");
                int gamelevel = read1.GetInt32("gamelevel");
                int money = read1.GetInt32("money");
                string bag = read1["bag"].ToString();
                if (name.Equals(L_If_Name.text))
                {
                    isHas = true;
                    Txt_money.text = money.ToString();
                    Txt_gamelevel.text = gamelevel.ToString();
                    PlayerPrefs.SetInt("money", money);
                    PlayerPrefs.SetInt("gamelevel", gamelevel);
                    PlayerPrefs.SetString("bag", bag);//把数据都保存下来
                }
            }
            read1.Close();
            MySqlDataReader read = sql.Select("20213908_衷茜芝_user");//调用Select函数
            //MySqlDataReader read1 = sql.Select("20213908_衷茜芝_gamedate");//调用Select函数  
            while(read.Read())
            {
                string name = read.GetString("name");
                string password=read.GetString("password");
                string sex = read["sex"].ToString();
                //int gamelevel = read1.GetInt32("gamelevel");
                //int money = read1.GetInt32("money");
                //string bag = read1["bag"].ToString();
                if (name.Equals(L_If_Name.text)) //
                {
                    isHas = true;
                    if(password.Equals(L_If_Password.text))
                    {
                        ShowRemainText("尊敬的" + name + "用户，欢迎回来！");
                        LogicPanel.SetActive(false);//关闭登录界面
                        gamePanel.SetActive(true);//打开游戏界面
                        Txt_username.text = name;
                        //Txt_money.text = money.ToString();
                        //Txt_gamelevel.text = gamelevel.ToString();
                        PlayerPrefs.SetString("name",name);
                        PlayerPrefs.SetString("password",password);
                        PlayerPrefs.SetString("sex",sex);
                        //PlayerPrefs.SetInt("money", money);
                        //PlayerPrefs.SetInt("gamelevel", gamelevel);
                        //PlayerPrefs.SetString("bag", bag);//把数据都保存下来
                        List<JsonData> data = new List<JsonData>()
                        {
                    new JsonData(){Password=PlayerPrefs.GetString("password")}
                        };
                        string json = JsonMapper.ToJson(data);
                        json = Regex.Unescape(json);//把Unescode转中文
                        DataSecurity dataSecurity = new DataSecurity();
                        json = dataSecurity.Encript(json);//字符串加密
                        L_If_Password.text = json;
                        Debug.Log(json);
                        List<JsonData> data1 = new List<JsonData>()
                        {
                          new JsonData(){Name=PlayerPrefs.GetString("name"),Password=PlayerPrefs.GetString("password")}
                        };
                        json = JsonMapper.ToJson(data1);
                        json = Regex.Unescape(json);//把Unescode转中文
                        json=dataSecurity.Encript(json);
                        File.WriteAllText(Application.dataPath + "/UserInfoPassword.txt", json, System.Text.Encoding.UTF8);//最后的为存储格式
                        json = dataSecurity.Decript(json);//将字符串解密
                        Debug.Log(json);
                        File.WriteAllText(Application.dataPath + "/UserInfo.txt", json, System.Text.Encoding.UTF8);//最后的为存储格式
                    }
                    else
                    {
                        ShowRemainText("密码输入错误,请重新输入!");
                    }
                    break;
                }
            }
            read.Close();
            if(isHas==false)
            {
                ShowRemainText("当前用户不存在，请先注册!");
            }
            sql.Close();
        });

        Btn_ToRegister.onClick.AddListener(() =>
        {
            LogicPanel.SetActive(false);
            RegisterPanel.SetActive(true);
        });

        Btn_Register.onClick.AddListener(() =>
        {
            if(R_If_Name.text == "" || R_If_Password.text == "")
            {
                ShowRemainText("账户和密码不能为空!!!");
                return;
            }
            if(string.IsNullOrWhiteSpace(R_If_Name.text)||string.IsNullOrWhiteSpace(R_If_Password.text))
            {
                ShowRemainText("账户密码不能为纯空格");
                return;
            }
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$");
            bool result = regex.IsMatch(R_If_Password.text);
            if (result == false)
            {
                ShowRemainText("密码最少6位，包括至少1个大写字母，1个小写字母，1个数字，1个特殊字符");
                return;
            }
            if (R_Password.text != R_If_Password.text)
            {
                ShowRemainText("两次输入的密码必须相同!");
                return;
            }
            MySQL sql = new MySQL();
            //MySqlDataReader read = sql.Select("20213908_衷茜芝_user");
            MySqlDataReader read = sql.Select("20213908_衷茜芝_gamedate");
            bool isRepeat = false;
            while(read.Read())
            {
                string name = read.GetString("name");
                if (name.Equals(R_If_Name.text))
                {
                    isRepeat = true;
                    ShowRemainText("当前用户已存在，请重新输入！");
                    break;
                }
            }
            read.Close();
            if(!isRepeat)
            {
                int sex = D_Sex.value + 1;//在Mysql中使用枚举类型从1开始赋值
                sql.InsertGamedate(R_If_Name.text);
                sql.InsertUser(R_If_Name.text, R_If_Password.text, sex);
            }
            sql.Close();
            SceneManager.LoadScene(0);//回到登陆界面
        });

        Btn_Back.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);//返回登录界面
        });

        B_set.onClick.AddListener(() =>
        {
            gamePanel.SetActive(false); 
            SCPanel.SetActive(true);

            Txt_SC_Name.text = PlayerPrefs.GetString("name");
            Txt_SC_Money.text =PlayerPrefs.GetInt("money").ToString();
            Txt_SC_GameLevel.text=PlayerPrefs.GetInt("gamelevel").ToString().ToLower(); 
        });

        B_GameStart.onClick.AddListener(() =>
        {
            isOpen = !isOpen;
            Card.SetActive(isOpen);//这里进行选人，其他在StartState里面写
        });

        B_Back.onClick.AddListener(() =>
        {
            gamePanel.SetActive(false);
            LogicPanel.SetActive(true);
        });

        B_Bag.onClick.AddListener(() =>
        {
            gamePanel.SetActive(false);
            BagPanel.SetActive(true);
        });

        B_Quest.onClick.AddListener(() =>
        {
            isOpen = !isOpen;
            gamePanel.transform.GetChild(10).gameObject.SetActive(isOpen);
           //创建FileStream对象
            FileStream fstream = new FileStream(Application.dataPath + "/说明卡.txt", FileMode.OpenOrCreate);
            byte[] bData = new byte[fstream.Length];
            //设置流当前位置为文件开始位置
            fstream.Seek(0, SeekOrigin.Begin);
            //将文件的内容存到字节数组中（缓存）
            fstream.Read(bData, 0, bData.Length);
            string result = Encoding.UTF8.GetString(bData);
            Debug.Log(result);
            gamePanel.transform.GetChild(10).GetComponent<Text>().text = result;
            if (fstream != null)
            {
                //清除此流的缓冲区，使得所有缓冲的数据都写入到文件中
                fstream.Flush();
                fstream.Close();
            }
        });

        //B_Deck.onClick.AddListener(() =>
        //{
        //    ShowText();
        //});

        Btn_SC_Delete.onClick.AddListener(() =>
        {
            CancelPanel.SetActive(true);
        });

        Btn_Can_Yes.onClick.AddListener(() =>
        {
            MySQL sql = new MySQL();
            sql.Delete("20213908_衷茜芝_user", "name", PlayerPrefs.GetString("name"));
            sql.Delete("20213908_衷茜芝_gamedate", "name", PlayerPrefs.GetString("name"));
            CancelPanel.SetActive(false);
            gamePanel.SetActive(true);
        });

        Btn_Can_No.onClick.AddListener(() =>
        {
            CancelPanel.SetActive(false);
        });

        Btn_SC_Update.onClick.AddListener(() =>
        {
            UpdatePanel.SetActive(true);
        });

        Btn_Up_Yes.onClick.AddListener(() =>
        {
            string sex = "男";
            switch (SC_Dp_Sex.value)
            {
                case 0:
                    sex = "男";
                    break;
                case 1:
                    sex = "女";
                    break;
                default:
                    break;
            }
            if (SC_If_Password.text.Equals(PlayerPrefs.GetString("password")) && sex.Equals(PlayerPrefs.GetString("sex")))
            {
                ShowRemainText("信息未作任何变动，请重新输入");
                return;
            }
            if (SC_If_Password.text == "")
            {
                ShowRemainText("密码不能为空!!!");
                return;
            }
            if (string.IsNullOrWhiteSpace(SC_If_Password.text))
            {
                ShowRemainText("密码不能为纯空格");
                return;
            }
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$");
            bool result = regex.IsMatch(SC_If_Password.text);
            if (result == false)
            {
                ShowRemainText("密码最少6位，包括至少1个大写字母，1个小写字母，1个数字，1个特殊字符");
                return;
            }
            if (SC_If_Password.text != SC_If_Password1.text)
            {
                ShowRemainText("两次输入的密码必须相同!");
                return;
            }
            MySQL sql = new MySQL();
            sql.UpdateDate("20213908_衷茜芝_user", new string[] { "password", "sex"},
                   new string[] { SC_If_Password.text, sex}, "name", PlayerPrefs.GetString("name"));

            sql.Close();
            UpdatePanel.SetActive(false);
            gamePanel.SetActive(true);
        });

        Btn_Up_No.onClick.AddListener(() =>
        {
            UpdatePanel.SetActive(false);
        });

        Btn_SC_Back.onClick.AddListener(() =>
        {
            SCPanel.SetActive(false);
            gamePanel.SetActive(true);
        });

        Bag_Back.onClick.AddListener(() =>
        {
            BagPanel.SetActive(false);
            gamePanel.SetActive(true);
        });

        Btn_explanation.onClick.AddListener(() =>
        {
            isOpen = !isOpen;
            BagPanel.transform.GetChild(2).gameObject.SetActive(isOpen);
           // 创建FileStream对象
        FileStream fstream = new FileStream(Application.dataPath + "/说明卡.txt", FileMode.OpenOrCreate);
            byte[] bData = new byte[fstream.Length];
            //设置流当前位置为文件开始位置
            fstream.Seek(0, SeekOrigin.Begin);
            //将文件的内容存到字节数组中（缓存）
            fstream.Read(bData, 0, bData.Length);
            string result = Encoding.UTF8.GetString(bData);
            Debug.Log(result);
            BagPanel.transform.GetChild(2).GetComponent<Text>().text = result;
            if (fstream != null)
            {
                //清除此流的缓冲区，使得所有缓冲的数据都写入到文件中
                fstream.Flush();
                fstream.Close();
            }
        });

        ShowData();
    }

    void ShowRemainText(string str)
    {
        text_Remain.gameObject.SetActive(true);
        text_Remain.text = str;
        Invoke("CloseRemainText", 2);//2s后调用这个方法
    }

    void CloseRemainText()
    {
        text_Remain.gameObject.SetActive(false);
        text_Remain.text = "";
    }

    private void ShowData()
    {
        if (PlayerPrefs.GetInt("SCORE1").Equals(10000))
        {
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(0).gameObject.SetActive(true);
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(0).GetComponent<Text>().text = "第一名"
                + PlayerPrefs.GetInt("SCORE1");
        }
        if (PlayerPrefs.GetInt("SCORE2").Equals(10000))
        {
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(1).gameObject.SetActive(true);
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(1).GetComponent<Text>().text = "第二名"
                + PlayerPrefs.GetInt("SCORE2");
        }
        if (PlayerPrefs.GetInt("SCORE3").Equals(10000))
        {
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(2).gameObject.SetActive(true);
            gamePanel.transform.GetChild(6).GetChild(1).GetChild(2).GetComponent<Text>().text = "第三名"
                + PlayerPrefs.GetInt("SCORE3");
        }
    }

  
}
