﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Database : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}

[System.Serializable]

// Класс объекта инвентаря.
public class Item
{
    public int id;
    public string name;
    public Sprite img;
}
