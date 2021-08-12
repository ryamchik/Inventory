using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryView : MonoBehaviour
{
    public Database data;
    private List<ItemInventory> pocket = new List<ItemInventory>();
    private List<ItemInventory> storage = new List<ItemInventory>();

    public GameObject gameObjShow;
    public GameObject InventoryLeftObject;
    public GameObject InventoryRightObject;
    public GameObject left_Slots;
    public GameObject right_Slots;

    public int maxCounts;

    public Camera cam;
    public EventSystem es;

    private InventoryPresenter presenter;
    private InventoryModel model;
    public int currentID;
    public ItemInventory currentItem;

    public void Start()
    {
        if (pocket.Count == 0 && storage.Count == 0)
        {
            AddGraphics();
        }

        this.model = new InventoryModel(pocket, storage, InventoryLeftObject, InventoryRightObject, left_Slots, right_Slots, maxCounts, gameObjShow, data);
        this.presenter = new InventoryPresenter(model);
    }

    public void Graph(int i, GameObject InventoryObject, List<ItemInventory> invEl)
    {
        GameObject newItem = Instantiate(gameObjShow, InventoryObject.transform) as GameObject;
        newItem.name = i.ToString();

        ItemInventory it_inv = new ItemInventory();
        it_inv.itemGameObj = newItem;

        Button tempButton = newItem.GetComponent<Button>();
        tempButton.onClick.AddListener(delegate { OnClick(); });

        invEl.Add(it_inv);
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCounts; i++)
        {
            Graph(i, InventoryLeftObject, pocket);
            Graph(i, InventoryRightObject, storage);
        }
    }

    // Реакция на клик по элементу.
    public void OnClick()
    {
        currentID = int.Parse(es.currentSelectedGameObject.name);
        currentItem = new ItemInventory(0, es.currentSelectedGameObject);

        model = presenter.Click(currentID, currentItem);
    }


    // Реакция на нажатие кнопки "Переместить".
    public void MoveItem()
    {
        model = presenter.MoveIt(currentID, currentItem);
    }
}