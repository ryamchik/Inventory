using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;

    public ItemInventory()
    {
    }

    public ItemInventory(int ID, GameObject item)
    {
        this.id = ID;
        this.itemGameObj = item;
    }
}
