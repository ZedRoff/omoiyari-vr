using System.Collections.Generic;
using UnityEngine;


public class Inventory
{
    public List<Item> items;

    public Inventory() {
        items = new List<Item>();
    }
    public List<Item> GetItems() {
        return items;
    }
    public int GetCount() {
        return items.Count;
    }
    public void AddItem(Item item) {
        items.Add(item);
    }
    public void RemoveItem(string item) {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item)
            {
                Debug.Log(i);
                items.RemoveAt(i);
                break;
            }
        }
    }
    public void FlushInventory() {
        items.Clear();
    }
    public Item GetItem(int index) {
        return items[index];
    }
    public bool HasItem(string itemName)
    {
        for(int i=0;i<items.Count;i++)
        {
            if (items[i].itemName == itemName) return true;
        }
        return false;
    }
}
