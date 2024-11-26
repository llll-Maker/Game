using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler
{


    public Transform originalParent;
    public Inventory myBag;
    public int currentItemID;//当前物品的ID
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;//原始的父级
        currentItemID=originalParent.GetComponent<Slot>().slotId;
        transform.SetParent(transform.parent.parent);// 升了一级
        transform.position= eventData.position;//获得鼠标的位置
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;//获得鼠标的位置
        Debug.Log(eventData.pointerCurrentRaycast. gameObject.name);//射线说碰到的物体名字
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
       if (eventData.pointerCurrentRaycast.gameObject != null)
       {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Image")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);//Image的parent是item，在一个parent才是slot
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //items的物品存储位置改变
                var temp = myBag.items[currentItemID];
                myBag.items[currentItemID] = myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId];//当前物品的id变成了鼠标点击的物体
                myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = temp;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中移动的物品
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                
                //否则直接挂在检测到Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //items的物品存储位置改变
                myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = myBag.items[currentItemID];
                //解决自己放在自己位置的问题
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotId == currentItemID)
                    myBag.items[currentItemID] = null;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        //其他任何位置都归位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

   
}
