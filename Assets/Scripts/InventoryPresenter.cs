using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPresenter
{
    private InventoryModel inv_model;

    public InventoryPresenter(InventoryModel model)
    {
        this.inv_model = model;
    }

    public InventoryModel Click(int currentID, ItemInventory currentItem)
    {
        this.inv_model.OnClick(currentID, currentItem);
        return inv_model;
    }
    public InventoryModel MoveIt(int currentID, ItemInventory currentItem)
    {
        this.inv_model.MoveItem(currentID, currentItem);
        return inv_model;
    }
}