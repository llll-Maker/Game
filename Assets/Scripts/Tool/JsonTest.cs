using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Data;

public class JsonTest : MonoBehaviour
{
    public InputField input;
    public Button button;
    private void Start()
    {
        List<JsonData> data = new List<JsonData>()
        {
            new JsonData(){Name=PlayerPrefs.GetString("name"),Password=PlayerPrefs.GetString("password")}
        };
        string json = JsonMapper.ToJson(data);
        json = Regex.Unescape(json);//把Unescode转中文
        DataSecurity dataSecurity = new DataSecurity();
        json = dataSecurity.Encript(json);//字符串加密
        File.WriteAllText(Application.dataPath + "/UserInfo.txt", json, System.Text.Encoding.UTF8);//最后的为存储格式
        Debug.Log(json);
        json = dataSecurity.Decript(json);//将字符串解密
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/UserInfo.txt", json, System.Text.Encoding.UTF8);//最后的为存储格式
 

    }
}
