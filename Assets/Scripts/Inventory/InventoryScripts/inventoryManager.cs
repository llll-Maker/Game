using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour
{
    public static inventoryManager instance;
    public Inventory myBag;
    public GameObject slotGrid;
    //public Slot slot;
    public GameObject emptySlot; 
    public Text itemInformation;
    public Image itemInformationImage;//描述的时候也会出现图片

    public List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    private void Start()
    {
       
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }
    public static void UpdateItemInfo(string itemDescription,Sprite item)
    {
        instance.itemInformation.text = itemDescription;
        instance.itemInformationImage.sprite = item;
        
    }
    //public static void CreatNewItem(item item)
    //{
    //    Slot newItem=Instantiate(instance.slot,instance.slot.transform.position,Quaternion.identity);
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //    newItem.slotItem = item;
    //    newItem.slotImage.sprite = item.itemImage;//传进图片
    //    newItem.slotNumber.text=item.itemHeld.ToString();//传进数据
    //}
    public static void RefreshItem()
    {
        //循环删除slotGrid下的子集物体
        for(int i=0;i<instance.slotGrid.transform.childCount;i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        Debug.Log(instance.myBag.items.Count);

        //重新生成对应myBag里面的物品slot
        for(int i=0;i<instance.myBag.items.Count;i++)
        {
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotId = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.items[i]);
        }
    }
}
