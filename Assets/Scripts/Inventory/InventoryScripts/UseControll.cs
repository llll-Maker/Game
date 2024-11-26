using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class UseControll : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        Parameter parameter = new Parameter();
        button = GameObject.Find("Canvas/bag").transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //Debug.Log(slotImage.sprite.name);
            for (int i = 0; i < inventoryManager.instance.myBag.items.Count; i++)
            {
                Debug.Log(inventoryManager.instance.myBag.items[i].itemImage.name);
                if (inventoryManager.instance.myBag.items[i].itemImage.name == "mushroom_yellow_tr" || inventoryManager.instance.myBag.items[i].itemImage.name == "蘑菇"
                || inventoryManager.instance.myBag.items[i].itemImage.name == "南瓜灯" ||
                inventoryManager.instance.myBag.items[i].itemImage.name == "仙人掌")
                {
                    
                    if (inventoryManager.instance.myBag.items[i].itemHeld == 0)
                    {
                        Destroy(inventoryManager.instance.myBag.items[i]);
                    }
                    int level = PlayerPrefs.GetInt("gamelevel");
                    level++;
                    MySQL sql = new MySQL();
                    sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "gamelevel" }, new string[] { level.ToString() }, "name", PlayerPrefs.GetString("name"));
                    //更新一下
                    PlayerPrefs.SetInt("gamelever", level);
                    sql.Close();
                    inventoryManager.instance.myBag.items[i].itemHeld--;
                }
                if (inventoryManager.instance.myBag.items[i].itemImage.name == "green_сlover_tr")
                {

                    if (inventoryManager.instance.myBag.items[i].itemHeld == 0)
                    {
                        Destroy(inventoryManager.instance.myBag.items[i]);
                    }
                    inventoryManager.instance.myBag.items[i].itemHeld--;
                }
                //技能药水
                if (inventoryManager.instance.myBag.items[i].itemImage.name == "potion_blue")
                {

                    if (inventoryManager.instance.myBag.items[i].itemHeld == 0)
                    {
                        Destroy(inventoryManager.instance.myBag.items[i]);
                    }
                    inventoryManager.instance.myBag.items[i].itemHeld--;
                }
                //速度药水
                if (inventoryManager.instance.myBag.items[i].itemImage.name == "potion_0")
                {

                    if (inventoryManager.instance.myBag.items[i].itemHeld == 0)
                    {
                        Destroy(inventoryManager.instance.myBag.items[i]);
                    }
                    CharacterControl.runspeed += 1f;
                    inventoryManager.instance.myBag.items[i].itemHeld--;
                }
                //伤害药水
                if (inventoryManager.instance.myBag.items[i].itemImage.name == "potion_red")
                {
                    Debug.Log(inventoryManager.instance.myBag.items[i].itemImage.name);
                    if (inventoryManager.instance.myBag.items[i].itemHeld == 0)
                    {
                        Destroy(inventoryManager.instance.myBag.items[i]);
                    }
                    Parameter.characterhp -= 20f;
                    inventoryManager.instance.myBag.items[i].itemHeld--;
                }
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
