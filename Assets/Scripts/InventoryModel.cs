using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryModel
{
    private Database data;

    private List<ItemInventory> pocket = new List<ItemInventory>();
    private List<ItemInventory> storage = new List<ItemInventory>();

    private GameObject objShow;
    private GameObject inventoryLeft;
    private GameObject inventoryRight;
    private GameObject lSlots;
    private GameObject rSlots;
    private int maxCount;
    private int count;
    private InventoryView view;
    private int currentID;
    private ItemInventory currentItem;

    public InventoryModel(List<ItemInventory> pock, List<ItemInventory> store, GameObject il, GameObject ir, GameObject ls, GameObject rs, int mc, GameObject os, Database db)
    {
        this.pocket = pock;
        this.storage = store;
        this.inventoryLeft = il;
        this.inventoryRight = ir;
        this.lSlots = ls;
        this.rSlots = rs;
        this.maxCount = mc;
        this.objShow = os;
        this.data = db;
        this.count = 0;

        // Заполнение ячеек панелей.
        for (int i = 0; i < maxCount; i++)
        {
            AddItem(i, data.items[Random.Range(0, data.items.Count)], pocket);
            AddItem(i, data.items[0], storage);
        }

        UpdateInventory(pocket);
        UpdateInventory(storage);
    }

    public void UpdateInventory(List<ItemInventory> invEl)
    {
        for (int i = 0; i < maxCount; i++)
        {
            invEl[i].itemGameObj.GetComponent<Image>().sprite = data.items[invEl[i].id].img;
        }
    }
    public ItemInventory CopyInventoryItem(ItemInventory oldy)
    {
        ItemInventory newy = new ItemInventory();

        newy.id = oldy.id;
        newy.itemGameObj = oldy.itemGameObj;

        return newy;
    }
    public void AddItem(int id, Item item, List<ItemInventory> invEl)
    {
        invEl[id].id = item.id;
        invEl[id].itemGameObj.GetComponent<Image>().sprite = data.items[item.id].img;
    }

    public void AddInventoryItem(int id, ItemInventory invItem, List<ItemInventory> invEl)
    {
        invEl[id].id = invItem.id;
        invEl[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;
    }

    // Реакция на клик по элементу.
    public void OnClick(int currentID, ItemInventory currentItem)
    {
        Button btn = GameObject.Find("Move_Button").GetComponent<Button>();
        Sprite[] spr = Resources.LoadAll<Sprite>("Interface");

        if (count == 0)
        {
            if (pocket[currentID].itemGameObj == currentItem.itemGameObj)
            {
                lSlots.transform.GetChild(currentID).transform.GetComponent<Image>().sprite = spr[8];
            }
            else
            {
                rSlots.transform.GetChild(currentID).transform.GetComponent<Image>().sprite = spr[8];
            }

            btn.interactable = true;
            this.count = 1;
        }

        else
        {
            if (pocket[currentID].itemGameObj == currentItem.itemGameObj)
            {
                lSlots.transform.GetChild(currentID).transform.GetComponent<Image>().sprite = spr[7];
            }
            else
            {
                rSlots.transform.GetChild(currentID).transform.GetComponent<Image>().sprite = spr[7];
            }

            btn.interactable = false;
            currentID = -1;
            count = 0;
        }
    }

    // Реакция на нажатие кнопки "Переместить".
    public void MoveItem(int currentID, ItemInventory currentItem)
    {
        if (currentID != -1)
        {
            if (currentItem.itemGameObj.transform.parent.name == inventoryLeft.name)
            {
                currentItem = CopyInventoryItem(pocket[currentID]);

                int i = storage.FindIndex(
                    store => store.itemGameObj.GetComponent<Image>().sprite == data.items[0].img
                );

                AddInventoryItem(currentID, pocket[i], pocket);
                AddInventoryItem(i, currentItem, storage);
                AddItem(currentID, data.items[0], pocket);
            }

            else
            {
                currentItem = CopyInventoryItem(storage[currentID]);

                int i = pocket.FindIndex(
                    pock => pock.itemGameObj.GetComponent<Image>().sprite == data.items[0].img
                );
                
                AddInventoryItem(currentID, storage[i], storage);
                AddInventoryItem(i, currentItem, pocket);

                AddItem(currentID, data.items[0], storage);
            }

            OnClick(currentID, currentItem);
        }

    }
}