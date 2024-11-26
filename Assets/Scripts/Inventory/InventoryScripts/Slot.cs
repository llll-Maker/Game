using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotId;//空格ID等于物品的ID
    public item slotItem;
    public Image slotImage;
    public Text slotNumber;
    public string slotInfo;
    public Sprite slotInfoImage;
    private Button button;
    private Button itemInformationButton;
    //public Inventory inventory;
    // public Image slotDescriptionImage;//在描述上面还有一层图片
    public GameObject itemInSlot;

    private void Start()
    {

        itemInformationButton = GameObject.Find("Canvas/bag").transform.GetChild(0).GetChild(2).GetComponent<Button>();
        itemInformationButton.onClick.AddListener(() =>
        {
            GameObject.Find("Canvas/bag").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        });
        button = GameObject.Find("Canvas/bag").transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>();
    }
    
    public void ItemOnClicked()
    {
        GameObject.Find("Canvas/bag").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        inventoryManager.UpdateItemInfo(slotInfo, slotInfoImage);
        button.onClick.AddListener(() =>
        {
            if (slotItem.itemName == "花朵" || slotItem.itemName == "蘑菇"
                 || slotItem.itemName == "南瓜" || slotItem.itemName == "仙人掌")
            {
                if (slotItem.itemHeld == 1)
                {
                    string str = PlayerPrefs.GetString("bag");
                    string str2 = "," + slotItem.itemName;
                   // Debug.Log(str);
                    string str1 = str.Replace(str2, "");
                   // Debug.Log(str1);
                    MySQL sql1 = new MySQL();
                   // sql1.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str1 }, "name", PlayerPrefs.GetString("name"));
                    sql1.Close();
                    //PlayerPrefs.SetString("bag", str1);
                    Destroy(gameObject);
                    inventoryManager.instance.myBag.items.Remove(slotItem);
                }
                int level = PlayerPrefs.GetInt("gamelevel");
                level++;
                MySQL sql = new MySQL();
                sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "gamelevel" }, new string[] { level.ToString() }, "name", PlayerPrefs.GetString("name"));
                //更新一下
                PlayerPrefs.SetInt("gamelevel", level);
                sql.Close();
                slotItem.itemHeld--;
                slotNumber.text = slotItem.itemHeld.ToString();
            }
            if (slotItem.itemName== "三叶草")
            {
                Debug.Log(PlayerPrefs.GetString("bag"));
                if (slotItem.itemHeld == 1)
                {
                    string str = PlayerPrefs.GetString("bag");
                    string str1=str.Replace("三叶草,", "");
                    Debug.Log(str1);
                    MySQL sql = new MySQL();
                    sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str1 }, "name", PlayerPrefs.GetString("name"));
                    sql.Close();
                    PlayerPrefs.SetString("bag", str1);
                    Destroy(gameObject);
                    inventoryManager.instance.myBag.items.Remove(slotItem);
                }
                slotItem.itemHeld--;
                if(Parameter.characterhp<Parameter.MaxHP)
                {
                    Parameter.characterhp += 20f;
                }
                
                Debug.Log(Parameter.characterhp);
                slotNumber.text = slotItem.itemHeld.ToString();
            }
            //技能药水
            if (slotItem.itemName == "技能药水")
            {

                if (slotItem.itemHeld == 1)
                {
                    string str = PlayerPrefs.GetString("bag");
                    string str1 = str.Replace(",技能药水", "");
                    MySQL sql = new MySQL();
                    sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str1 }, "name", PlayerPrefs.GetString("name"));
                    sql.Close();
                    PlayerPrefs.SetString("bag", str1);
                    Destroy(gameObject);
                    inventoryManager.instance.myBag.items.Remove(slotItem);
                }
                CharacterControl.attack -= 2f;
                CharacterControl.attackcd -= 2f;
                slotItem.itemHeld--;
                slotNumber.text = slotItem.itemHeld.ToString();
            }
            //速度药水
            if (slotItem.itemName == "速度药水")
            {

                if (slotItem.itemHeld == 1)
                {
                    string str = PlayerPrefs.GetString("bag");
                    string str1 = str.Replace(",速度药水", "");
                    MySQL sql = new MySQL();
                    sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str1 }, "name", PlayerPrefs.GetString("name"));
                    sql.Close();
                    PlayerPrefs.SetString("bag", str1);
                    Destroy(gameObject);
                    inventoryManager.instance.myBag.items.Remove(slotItem);
                }
                CharacterControl.runspeed += 1f;
                slotItem.itemHeld--;
                slotNumber.text = slotItem.itemHeld.ToString();
            }
            //伤害药水
            if (slotItem.itemName == "伤害药水")
            {

                if (slotItem.itemHeld == 1)
                {
                    string str = PlayerPrefs.GetString("bag");
                    string str1 = str.Replace(",伤害药水", "");
                    MySQL sql = new MySQL();
                    sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str1 }, "name", PlayerPrefs.GetString("name"));
                    sql.Close();
                    PlayerPrefs.SetString("bag", str1);
                    Destroy(gameObject);
                    inventoryManager.instance.myBag.items.Remove(slotItem);
                }
                Parameter.characterhp -= 20f;
                slotItem.itemHeld--;
                slotNumber.text = slotItem.itemHeld.ToString();
            }
        });
    }

    public void SetupSlot(item item)
    {
        if(item==null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotItem = item;
        slotImage.sprite=item.itemImage;
        slotNumber.text = item.itemHeld.ToString();
        slotInfo=item.information;
        slotInfoImage = item.itemImage;
       
    }
}
