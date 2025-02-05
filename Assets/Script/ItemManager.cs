using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class ItemManager
{
    public int item_Id;
    public string item_Name;
    public string item_Description;
    public int item_Count;
    public Sprite item_Icon;
    public ItemType item_Type;
    
    public static Dictionary<int, ItemManager> itemData = new Dictionary<int, ItemManager>();

    public enum ItemType
    {
        Item,
        Key_Item,
        Post_it
    }

    public ItemManager(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        item_Id = _itemID;
        item_Name = _itemName;
        item_Description = _itemDes;
        item_Type = _itemType;
        item_Count = _itemCount;

        item_Icon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;

        if (!itemData.ContainsKey(_itemID))
        {
            itemData.Add(_itemID, this);
        }
    }

    public static ItemManager GetItem(int id)
    {
        if (itemData.TryGetValue(id, out ItemManager item))
            return item;

        return null;
    }

    public static void InitializeItems()
    {
        new ItemManager(10001, "돌멩이", "평범한 돌이다.", ItemType.Item);
    }
}



