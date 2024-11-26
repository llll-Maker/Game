using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="NewItem",menuName="Inventory/New Item")]
public class item : ScriptableObject
{
    public string itemName;//物品名称
    public Sprite itemImage;//物品图片
    public int itemHeld;//现在有多少个
    [TextArea]
    public string information;//物品描述
}
