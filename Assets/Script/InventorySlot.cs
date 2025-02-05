using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemManager itemManger;

    public Image icon;
    public Text item_Name_Text;
    public Text item_Count_Text;
    public GameObject selected_Item;

    public void Additem(ItemManager _item)
    {
        item_Name_Text.text = _item.item_Name;
        icon.sprite = _item.item_Icon;
        if (_item.item_Type.ToString() == "Item")
        {
            if (_item.item_Count > 0)
                item_Count_Text.text = ": " + _item.item_Count.ToString();
            else
                item_Count_Text.text = "";
        }
    }


    public void RemoveItem()
    {
        item_Name_Text.text = "";
        item_Count_Text.text = "";
        icon.sprite = null;
    }
}
