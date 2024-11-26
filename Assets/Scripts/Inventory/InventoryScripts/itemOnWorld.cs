using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    public item thisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("Award", 3);
            other.GetComponent<AudioSource>().Play();
            Debug.Log(1111);
            AddNewItem();//在背包中添加
            Destroy(gameObject);//销毁物体
        }
    }
    public void AddNewItem()
    {
        if(!playerInventory.items.Contains(thisItem))
        {
            //playerInventory.items.Add(thisItem);//添加物体
            //inventoryManager.CreatNewItem(thisItem);//在背包中显示
            for(int i=0;i<playerInventory.items.Count;i++)
            {
                if (playerInventory.items[i] == null)
                {
                    playerInventory.items[i] = thisItem;
                    MySQL sql = new MySQL();
                    string str = PlayerPrefs.GetString("bag");
                    string str1 = str + "," + thisItem.itemName;
                    sql.UpdateDate("20213908_衷茜芝_gamedate", new string[] { "bag" }, new string[] { str1 }, "name", PlayerPrefs.GetString("name"));
                    sql.Close();
                    PlayerPrefs.SetString("bag",str1);
                    break;
                }

            }
        }
        else
        {
            thisItem.itemHeld += 1;//持有数量加1
        }
        inventoryManager.RefreshItem();
    }
    
}
