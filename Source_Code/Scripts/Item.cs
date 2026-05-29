using UnityEngine;

public class Item
{
    public string itemName;
    public string itemDescription;
    public Sprite icon;

    public bool usable;
    
    public Item(string name, string description, Sprite sprite, bool pUsable=false) {
        itemName = name;
        itemDescription = description;
        icon = sprite;
        usable = pUsable;
    }
    
}
