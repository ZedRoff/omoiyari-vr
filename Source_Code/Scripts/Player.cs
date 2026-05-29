
using UnityEngine;

public class Player
{
    public Inventory inventory;
    public Item currentItem;
    public Sprite noItemSprite;
    public Player() {
        inventory = new Inventory();
        currentItem = new Item("NoItem", "no item", noItemSprite);
    }
    
    
}
